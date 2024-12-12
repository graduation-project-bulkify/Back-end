using Bulkify.Core.Entities;
using Bulkify.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(object user, string role)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, GetUserId(user).ToString()),
            new Claim(JwtRegisteredClaimNames.Email, GetUserEmail(user)),
            new Claim(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            // Use JsonWebTokenHandler
            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }

        private int GetUserId(object user)
        {
            if (user is Customer customer)
            {
                return customer.Id;
            }
            else if (user is Supplier supplier)
            {
                return supplier.Id;
            }
            else
            {
                throw new ArgumentException("Invalid user type.");
            }
        }

        private string GetUserEmail(object user)
        {
            if (user is Customer customer)
            {
                return customer.Email;
            }
            else if (user is Supplier supplier)
            {
                return supplier.Email;
            }
            else
            {
                throw new ArgumentException("Invalid user type.");
            }
        }
    }

}
