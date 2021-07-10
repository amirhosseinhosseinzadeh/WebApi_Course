namespace HotelListing.Models
{
    public class CreateRoleModel
    {
        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
    public class RoleModel : CreateRoleModel
    {
        public int Id { get; set; }
    }
}