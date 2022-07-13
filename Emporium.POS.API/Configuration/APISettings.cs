using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Configuration
{
    public class APISettings
    {
        public static string AuthSecret { get; set; }

        public static string RedisConnection { get; set; }
        public static string RedisInstanceName { get; set; }
        public static string RedisPort { get; set; }

        public static int TokenValidityDays { get; set; }
        public static int TokenValidityMinutes { get; set; }

        public static bool OtpByEmail { get; set; }
        public static bool OtpBySms { get; set; }
        public static int OtpValiditySeconds { get; set; }
        public static int OtpLength { get; set; }
    }
}
