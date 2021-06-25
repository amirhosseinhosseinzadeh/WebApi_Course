using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Domains
{
    public class Country
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string ShortName { get; set; }

        [NotMapped]
        public virtual IList<Hotel> HotelList { get; set; }
    }
}