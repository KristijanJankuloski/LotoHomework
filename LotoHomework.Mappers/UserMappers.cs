using LotoHomework.Domain.Models;
using LotoHomework.DTOs.UserDTOs;

namespace LotoHomework.Mappers
{
    public static class UserMappers
    {
        public static User ToUser(this UserRegisterDto dto, string hash)
        {
            return new User
            {
                Username = dto.Username,
                FullName = dto.FullName,
                PasswordHash = hash
            };
        }
    }
}
