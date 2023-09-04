using LotoHomework.DataAccess.Repositories.Interfaces;
using LotoHomework.Domain.Models;
using LotoHomework.Domain.Statics;
using LotoHomework.DTOs.UserDTOs;
using LotoHomework.Mappers;
using LotoHomework.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace LotoHomework.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> LoginUser(UserLoginDto dto)
        {
            User user = await _userRepository.LoginAsync(dto.Username, HashPassowrd(dto.Password));
            if(user == null)
            {
                throw new UnauthorizedAccessException("Username or passowrd is incorrect");
            }

            return GenerateToken(user);
        }

        public async Task RegisterUser(UserRegisterDto dto, string role)
        {
            await ValidateUser(dto);

            string hash = HashPassowrd(dto.Password);
            User user = dto.ToUser(hash);
            user.Role = role;

            await _userRepository.InsertAsync(user);
        }

        private async Task ValidateUser(UserRegisterDto dto)
        {
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.FullName))
            {
                throw new ArgumentException("One or more values are empty");
            }

            if(dto.Password != dto.ConfirmPassword)
            {
                throw new ArgumentException("Password confirmation does not match");
            }

            User user = await _userRepository.GetByUsernameAsync(dto.Username);
            if(user != null)
            {
                throw new ArgumentException("Username is taken");
            }
        }

        private string HashPassowrd(string password)
        {
            MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = cryptoServiceProvider.ComputeHash(passwordBytes);
            return Encoding.ASCII.GetString(hashBytes);
        }

        private string GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] secret = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            int expireTime = Convert.ToInt32(_configuration["Jwt:Expire"]);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddMinutes(expireTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.Role)
                })
            };

            SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
