using Emporium.POS.API.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Emporium.POS.API.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly ILogger logger;

        public EncryptionService(ILogger logger)
        {
            this.logger = logger;
        }

        public string EncryptValue(string encryptingValue, string key)
        {
            string salt = CreateSalt(key);
            string saltAndPwd = String.Concat(encryptingValue, salt);
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));
            string hashedPwd = String.Concat(byteArrayToString(hashedDataBytes), salt);
            return hashedPwd;
        }

        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        private static string CreateSalt(string UserName)
        {
            string username = UserName;
            byte[] userBytes;
            string salt;
            userBytes = ASCIIEncoding.ASCII.GetBytes(username);
            long XORED = 0x00;

            foreach (int x in userBytes)
                XORED = XORED ^ x;

            Random rand = new Random(Convert.ToInt32(XORED));
            salt = rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            return salt;
        }

        public string DecrryptValue(string decryptingValue)
        {
            throw new NotImplementedException();
        }

       
    }
}
