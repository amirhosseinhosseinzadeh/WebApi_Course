using HotelListing.Data;
using HotelListing.Models;
using System.Threading.Tasks;
    
namespace HotelListing.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUserAsync(LoginModel loginModel);

        Task<string> GenerateTokenAsync(ApiUser user);
    }
}