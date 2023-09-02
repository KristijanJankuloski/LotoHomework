using LotoHomework.Domain.Models;

namespace LotoHomework.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> LoginAsync(string username, string hash);
    }
}
