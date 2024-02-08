using Microsoft.AspNetCore.Identity;
using StudentCRUD.DTOs.Shared;
using StudentCRUD.DTOs.Student;
using StudentCRUD.DTOs.UserAccount;
using StudentCRUD.Models;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentCRUD.Services.UserAccount
{
    public class UserService : IUserService
    {
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        IConfiguration _config;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ServiceResponse<GetUserRegisterDto>> CreateAccountAsync(AddUserRegisterDto registerDTO)
        {
            var serviceresponse = new ServiceResponse<GetUserRegisterDto>();
            var newUser = new ApplicationUser()
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                PasswordHash = registerDTO.Password,
                UserName = registerDTO.Email
            };

            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user is not null)
            {
                serviceresponse.Message = "User registered already";
                serviceresponse.Success = false;
                return serviceresponse;
            }

            var createUser = await _userManager.CreateAsync(newUser!, registerDTO.Password);

            if (!createUser.Succeeded)
            {
                serviceresponse.Message = "Something went wrong. please try again";
                serviceresponse.Success = false;
                return serviceresponse;
            }

            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _userManager.AddToRoleAsync(newUser, "Admin");
                serviceresponse.Message = "Account Created";
                return serviceresponse;
            }
            else
            {
                var checkUser = await _roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await _userManager.AddToRoleAsync(newUser, "User");
                serviceresponse.Message = "Account Created";
                return serviceresponse;
            }
        }

        public async Task<ServiceResponse<GetUserLoginDto>> LoginAccountAsync(UserLoginDto loginDTO)
        {
            var serviceresponse = new ServiceResponse<GetUserLoginDto>();

            var getUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
            {
                serviceresponse.Message = "User not found";
                serviceresponse.Success = false;
                return serviceresponse;
            }

            bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
            {
                serviceresponse.Message = "Invalid email/password";
                serviceresponse.Success = false;
                return serviceresponse;
            }

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());

            string token = GenerateToken(userSession);
            GetUserLoginDto getUserLogin = new GetUserLoginDto { Token = token };
            serviceresponse.Message = "Login completed";
            serviceresponse.data = getUserLogin;
            return serviceresponse;
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
