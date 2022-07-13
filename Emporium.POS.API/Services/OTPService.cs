using Emporium.POS.API.Configuration;
using Emporium.POS.API.Interfaces;
using Emporium.POS.API.Models.OTP;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Services
{
    public class OTPService : IOTPService
    {
        private readonly ILogger logger;
        private readonly IDistributedCache distributedCache;
        private readonly ITokenService tokenService;

        public OTPService(ILogger logger, IDistributedCache distributedCache, ITokenService tokenService)
        {
            this.logger = logger;
            this.distributedCache = distributedCache;
            this.tokenService = tokenService;
        }

        public async Task<OtpSendResponse> SendOtp(OtpSendRequest request)
        {
            try
            {
                logger.LogInformation("Sending OTP");

                var otp = generateOtp();
                var clientID = Guid.NewGuid().ToString();

                checkNull(request.Email, "Email");
                checkNull(request.Number, "Number");
                
                if (!APISettings.OtpBySms && !APISettings.OtpByEmail)
                {
                    throw new Exception("OTPs not configured");
                }

                var otpSent = await sendOtp(request, otp);

                if (otpSent)
                {
                    var otpRecord = createOtpRecord(clientID, otp);
                    var otpRecordString = JsonConvert.SerializeObject(otpRecord);
                    var cacheEntryOptions = new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpiration = otpRecord.ExpirationDate
                    };

                    await distributedCache.SetStringAsync(clientID, otpRecordString, cacheEntryOptions);

                    logger.LogInformation("OTP sent successfully");

                    return new OtpSendResponse()
                    {
                        HasError = false,
                        ClientID = clientID,
                        Message = "OTP sent successfully"
                    };
                }
                else
                {
                    var message = createOtpErrorMessage();

                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to send OTP");
                throw;
            }
        }

        public async Task<OtpVerifyResponse> VerifyOtp(OtpVerifyRequest request)
        {
            try
            {
                logger.LogInformation("Verifying OTP");

                checkNull(request.ClientID, "ClientID");
                checkNull(request.Otp, "Otp");

                var clientID = request.ClientID;
                var otpRecordString = await distributedCache.GetStringAsync(clientID);

                if (string.IsNullOrEmpty(otpRecordString))
                {
                    logger.LogError($"Unable to read OTP with client ID '{clientID}' from Redis");

                    throw new Exception("Invalid or expired OTP");
                }

                var otpRecord = JsonConvert.DeserializeObject<OtpRecord>(otpRecordString);
                var currentDate = DateTime.Now;

                if (currentDate > otpRecord.ExpirationDate)
                {
                    await distributedCache.RemoveAsync(clientID);
                    throw new Exception("The OTP has expired");
                }

                if (request.Otp.Equals(otpRecord.Otp))
                {
                    await distributedCache.RemoveAsync(clientID);

                    var token = tokenService.GetToken();

                    logger.LogInformation("OTP verified successfully");

                    return new OtpVerifyResponse()
                    {
                        HasError = false,
                        Message = "OTP verified successfully",
                        Token = token
                    };
                }
                else
                {
                    throw new Exception("Invalid or expired OTP");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to verify OTP");
                throw;
            }
        }

        private async Task<bool> sendOtp(OtpSendRequest request, string otp)
        {
            var otpSent = false;

            if (APISettings.OtpBySms)
            {
                try
                {
                    sendSmsOtp(request, otp);
                    otpSent = true;
                    logger.LogInformation("OTP sent successfully via SMS");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Failed to send OTP via SMS");
                }
            }

            if (APISettings.OtpByEmail)
            {
                try
                {
                    await sendEmailOtp(request, otp);
                    otpSent = true;
                    logger.LogInformation("OTP sent successfully via e-mail");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Failed to send OTP via e-mail");
                }
            }

            return otpSent;
        }

        private void sendSmsOtp(OtpSendRequest request, string otp)
        {
            //logger.LogInformation("Sending OTP via SMS");

            //TwilioClient.Init(ApiSettings.SmsAccountSid, ApiSettings.SmsAuthToken);

            //var message = MessageResource.Create(
            //    body: $"Your {request.CompanyName} OTP is {otp}",
            //    from: new Twilio.Types.PhoneNumber(ApiSettings.SmsFromNumber),
            //    to: new Twilio.Types.PhoneNumber(request.Number)
            //);

            //if (!string.IsNullOrEmpty(message.ErrorMessage))
            //{
            //    logger.LogError($"Failed to send SMS to number: {request.Number}\nError code: {message.ErrorCode}\nError message: {message.ErrorMessage}");
            //    throw new Exception("Failed to send OTP via SMS");
            //}
        }

        private async Task sendEmailOtp(OtpSendRequest request, string otp)
        {
            //logger.LogInformation("Sending OTP via e-mail");

            //var client = new SendGridClient(APISettings.SendGridKeyEmail);
            //var companyName = request.CompanyName;

            //var msg = new SendGridMessage()
            //{
            //    From = new EmailAddress(companyName.Replace(" ", "") + APISettings.EmailFrom, companyName),
            //    Subject = $"{companyName.ToUpper()} OTP!",
            //    PlainTextContent = $"Your {companyName} OTP is {otp}",
            //    HtmlContent = $"<strong>Your {companyName} OTP is {otp}</strong>"
            //};

            //msg.AddTo(new EmailAddress(request.Email, $"{companyName} OTP"));

            //var result = await client.SendEmailAsync(msg);

            //if (result.StatusCode != HttpStatusCode.Accepted)
            //{
            //    logger.LogError($"Unable to send email to email address: {request.Email} - status code {result.StatusCode}");
            //    throw new Exception(result.StatusCode.ToString());
            //}
        }

        private OtpRecord createOtpRecord(string clientID, string otp)
        {
            var currentDate = DateTime.Now;

            var record = new OtpRecord()
            {
                ClientID = clientID,
                Otp = otp,
                CreatedDate = currentDate,
                ExpirationDate = currentDate.AddSeconds(APISettings.OtpValiditySeconds)
            };

            return record;
        }

        private string createOtpErrorMessage()
        {
            var message = "";

            if (APISettings.OtpBySms && APISettings.OtpByEmail)
            {
                message = "Both the SMS and the e-mail failed to send";
            }
            else if (APISettings.OtpBySms)
            {
                message = "SMS failed to send";
            }
            else if (APISettings.OtpByEmail)
            {
                message = "E-mail failed to send";
            }

            return message;
        }

        private string generateOtp()
        {
            var random = new Random();
            var chars = "0123456789";
            
            var otp = new string(Enumerable.Repeat(chars, APISettings.OtpLength).Select(s => s[random.Next(s.Length)]).ToArray());

            return otp;
        }

        private void checkNull(object value, string name)
        {
            if (value is null || (value is string && string.IsNullOrEmpty(value.ToString())))
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
