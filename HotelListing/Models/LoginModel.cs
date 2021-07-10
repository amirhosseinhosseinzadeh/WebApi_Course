using Newtonsoft.Json;

namespace HotelListing.Models
{
    public class LoginModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Passwrod { get; set; }
    }
}