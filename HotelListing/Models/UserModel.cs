using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class UserModel
    {

        [DataType(DataType.PhoneNumber)]
        [JsonProperty("phonenumber")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("firstname")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty("lastname")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [JsonProperty("password")]
        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Passwrod { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

    }
    public class SignInModel : UserModel
    {

        [JsonProperty("usedefaultusername")]
        public bool UseDefaultUserName { get; set; } = true;

        public IList<int> UserRoles { get; set; }
    }
}