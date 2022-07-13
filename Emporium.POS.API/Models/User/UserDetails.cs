using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.User
{
    [Serializable]
    public class UserDetails
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "UserNoAsUserName")]
        public bool UserAsUserName { get; set; }

        [JsonProperty(PropertyName = "MobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty(PropertyName = "EmailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "Gendar")]
        public string Gendar { get; set; }

        [JsonProperty(PropertyName = "Language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "CreatedTime")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty(PropertyName = "LastLoggedIn")]
        public DateTime LastLoggedIn { get; set; }

        [JsonProperty(PropertyName = "UserType")]
        public string UserType { get; set; }
    }
}
