using Emporium.POS.API.Models.OTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Interfaces
{
    public interface IOTPService
    {
        Task<OtpSendResponse> SendOtp(OtpSendRequest request);

        Task<OtpVerifyResponse> VerifyOtp(OtpVerifyRequest request);
    }
}
