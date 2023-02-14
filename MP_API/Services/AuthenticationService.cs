using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using MP_API.Data;
using MP_API.Models;
using MP_API.Models.InputModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MP_API.Services
{
    public class AuthenticationService
    {
        internal readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public AuthenticationToken? Authenticate(User user)
        {
            var u = _context.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (u == null)
            {
                return null;
            }
            return CreateAuthenticationToken(u);
        }

        public async Task<User> Register(RegisterIM values)
        {
            User user = new User
            {
                Username = values.Username,
                Password = values.Password,
                FirstName = values.FirstName,
                LastName = values.LastName,
                Admin = values.Admin

            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private AuthenticationToken CreateAuthenticationToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            int validityDuration = 10;
            int.TryParse(_configuration["JWT:Expiration"], out validityDuration);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Username),
                    new("sub", user.UserId.ToString())
                }),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(validityDuration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationToken()
            {
                Name = "authorization_token",
                Value = tokenHandler.WriteToken(token),
            };
        }
    }
}