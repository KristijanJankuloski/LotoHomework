using LotoHomework.DTOs.UserDTOs;

namespace LotoHomework.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(UserRegisterDto dto, string role);
        Task<string> LoginUser(UserLoginDto dto);
    }
}
