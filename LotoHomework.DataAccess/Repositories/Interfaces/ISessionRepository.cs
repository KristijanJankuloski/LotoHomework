using LotoHomework.Domain.Models;

namespace LotoHomework.DataAccess.Repositories.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<Session> GetLatestActiveAsync();
        Task<Session> GetLastEndedAsync();
    }
}
