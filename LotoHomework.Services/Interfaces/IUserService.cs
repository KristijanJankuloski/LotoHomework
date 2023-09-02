using LotoHomework.DTOs.UserDTOs;

namespace LotoHomework.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(UserRegisterDto dto);
        Task<string> LoginUser(UserLoginDto dto);
    }
}
