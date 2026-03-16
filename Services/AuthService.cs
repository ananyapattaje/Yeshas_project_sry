using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.DTO;
using CAPGEMINI_CROPDEAL.Interfaces;
using CAPGEMINI_CROPDEAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CAPGEMINI_CROPDEAL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly CropDealDbContext _context;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            CropDealDbContext context)
        {
            _userManager = userManager;
            _config = config;
            _context = context;
        }

        public async Task<string> Register(RegisterDTO dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            await _userManager.AddToRoleAsync(user, dto.Role);

            if (dto.Role == "Farmer")
            {
                var farmer = new Farmer
                {
                    FarmerName = dto.FullName,
                    FarmerGmail = dto.Email,
                    PhoneNo = dto.PhoneNo,
                    Location = dto.Location,
                    UserId = user.Id
                };

                _context.Farmers.Add(farmer);
            }

            if (dto.Role == "Buyer")
            {
                var buyer = new Buyer
                {
                    BuyerName = dto.FullName,
                    BuyerGmail = dto.Email,
                    PhoneNo = dto.PhoneNo,
                    UserId = user.Id
                };

                _context.Buyers.Add(buyer);
            }

            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        public async Task<string> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new Exception("User not found");

            var valid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!valid)
                throw new Exception("Invalid password");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email!)
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddHours(2),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}