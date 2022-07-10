using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MovieTheater.Utilities.Helper
{
    public class JwtAuthenticationManager : JwtAuthenticationManager.IJwtAuthenticationManager
    {
        public interface IJwtAuthenticationManager
        {
            string Authenticate(string username, string password);
        }

        private readonly string _tokenKey;

        public JwtAuthenticationManager(string tokenKey)
        {
            this._tokenKey = tokenKey;
        }

        public string Authenticate(string username, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
