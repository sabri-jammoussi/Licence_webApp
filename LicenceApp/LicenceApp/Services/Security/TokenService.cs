using LicenceApp.models.GlobalDao;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LicenceApp.Services.Security
{
    public class TokenService
    {
        private const int ExpirationMinutes = 150;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public string CreateToken(UserDao user)
        {
            ValidateConfiguration();
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.LogInformation("JWT Token created");
            return tokenHandler.WriteToken(token);
        }
        private void ValidateConfiguration()
        {
            var requiredSettings = new[]
            {
                "JwtTokenSettings:ValidIssuer",
                "JwtTokenSettings:ValidAudience",
                "JwtTokenSettings:SymmetricSecurityKey"
            };

            foreach (var setting in requiredSettings)
            {
                if (string.IsNullOrEmpty(_configuration[setting]))
                {
                    throw new InvalidOperationException($"Configuration setting '{setting}' is missing.");
                }
            }
        }
        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
       DateTime expiration) =>
       new(
           new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["ValidIssuer"],
           new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["ValidAudience"],
           claims,
           expires: expiration,
           signingCredentials: credentials
           );
        private List<Claim> CreateClaims(UserDao user)
        {
            var jwtSub = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["JwtRegisteredClaimNamesSub"];

            try
            {
                var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, jwtSub),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),  // Adjust this line to use user properties directly
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        private SigningCredentials CreateSigningCredentials()
        {
            var symmetricSecurityKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["SymmetricSecurityKey"];

            return new SigningCredentials(
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey)),
               SecurityAlgorithms.HmacSha256
           );
        }

    }
}
