using LotoHomework.Domain.Models;

namespace LotoHomework.Services.Interfaces
{
    public interface ISessionService
    {
        Task<bool> StartSession();
        Task<Session> GetLatest();
    }
}
