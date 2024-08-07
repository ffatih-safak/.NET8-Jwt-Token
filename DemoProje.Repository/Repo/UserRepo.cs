using DemoProje.Core.Contracts;
using DemoProje.Core.DTOs;
using DemoProje.Entitiy.Entities;
using DemoProje.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoProje.Repository.Repo
{
    internal class UserRepo : IUser
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        public UserRepo(AppDbContext appDbContext,IConfiguration configuration )
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }
        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO)
        {
            var getUser = await FindUserByEmail(loginDTO.Email!);
            if(getUser == null)
            {
                return new LoginResponse(false, "User not found");
            }
            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.Password);
            if (checkPassword) {
                return new LoginResponse(true, "Login success", GenerateJwtToken(getUser));
            }
            else
            {
                return new LoginResponse(false, "Invalid ");
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var userClaims = new[]
             {
                 new Claim(ClaimTypes.Email, user.Email!),
                 new Claim(ClaimTypes.Name , user.Name!),
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
             };


            var token = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Audience"],
               claims: userClaims,
               expires: DateTime.Now.AddHours(1),
               signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        public async Task<RegistrationResponse> RegiserUserAsync(RegisterUserDTO registerUserDTO)
        {
            var getUser = await FindUserByEmail(registerUserDTO.Email!);
            if (getUser != null)
            {
                return new RegistrationResponse(false, "User already exist");
            }
            _appDbContext.Users.Add(new ApplicationUser()
            {
                Name = registerUserDTO.Name,
                Email = registerUserDTO.Email,  
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password)
            });
            await _appDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Register Complate");
        }
    }
}
