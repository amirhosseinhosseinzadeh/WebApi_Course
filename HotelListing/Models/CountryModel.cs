using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CountryModel : CreateCountryModel
    {
        public int Id { get; set; }

        public IList<HotelModel> HotelList { get; set; }
    }
    public class CreateCountryModel
    {
        [StringLength(maximumLength: 30, ErrorMessage = "Name must be atleast 5 and at max 30 characters", MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        [StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "Short name must be atleast 3 and at max 15 charactere")]
        [Required]
        public string ShortName { get; set; }
    }
}