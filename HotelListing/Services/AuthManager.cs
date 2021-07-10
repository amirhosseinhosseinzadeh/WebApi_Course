using System;
using System.Text;
using HotelListing.Data;
using HotelListing.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace HotelListing.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;

        private readonly IConfiguration _configuration;

        public AuthManager(UserManager<ApiUser> usermanager,
                           IConfiguration configuration
        )
        {
            this._userManager = usermanager;
            this._configuration = configuration;
        }

        private SigningCredentials GetSigninCredentials()
        {
            var key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IList<Claim>> GetClaimsAsync(ApiUser user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            var roles = await _userManager.GetRolesAsync(user);
            for (var i = 0; i <= roles.Count; i++)
            {
                claims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }
            return claims;
        }

        private SecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IList<Claim> claims)
        {
            var jwtSetting = _configuration.GetSection("Jwt");
            var expiraion = DateTime.Now.AddMinutes(double.Parse(jwtSetting.GetSection("LifeTime").Value));
            var token = new JwtSecurityToken(
                issuer: jwtSetting.GetSection("Issuer").Value,
                claims: claims,
                expires: expiraion,
                signingCredentials: signingCredentials
            );
            return token;
        }

        public async Task<string> GenerateTokenAsync(ApiUser user)
        {
            var signInCredentials = GetSigninCredentials();
            var claimList = await GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signInCredentials, claimList);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> ValidateUserAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user != null)
            {
                var validationResult = await _userManager.CheckPasswordAsync(user, loginModel.Passwrod);
                return validationResult;
            }
            return false;
        }
    }
}