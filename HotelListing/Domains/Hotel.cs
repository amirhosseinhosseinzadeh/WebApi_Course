using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Domains
{
    public class Hotel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public float Rate { get; set; }
        
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        
        public Country Countries { get; set; }
    }
}