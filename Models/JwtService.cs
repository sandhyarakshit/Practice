using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace SchoolApplication.Models
{
    public class JwtService
    {
        public string SecretKey { get; set; }
        public int TokenDuration { get; set; }
        private readonly IConfiguration config;
        public JwtService(IConfiguration _config)
        {
            config = _config;
            this.SecretKey = config.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);
        }

        public String GenerateToken (string id, string firstname , string lastname, string email , string mobile , string gender)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));

            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim("id", id),
                new Claim ("firstname", firstname),
                new Claim("lastname" , lastname),
                new Claim ("email", email),
                new Claim("mobile", mobile),
                new Claim ("gender", gender)
            };
            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
                );
            //JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
           
            //return handler.WriteToken(jwtToken);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
           
        }

    }
}
