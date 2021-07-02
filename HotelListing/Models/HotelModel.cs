using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HotelListing.Models
{
    public class HotelModel : CreateHotelModel
    {
        public int Id { get; set; }

        public CountryModel Country { get; set; }
    }
    public class CreateHotelModel
    {
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 5, ErrorMessage = "Name must be atleast 5 and at max 40 cahracter")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "Address must be atleast 5 and at max 100 cahracter")]
        public string Address { get; set; }

        [JsonProperty("rate")]
        [Range(1, 5)]
        public float Rate { get; set; }

        [JsonProperty("country_id")]
        public int CountryId { get; set; }
    }
}