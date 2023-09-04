using LotoHomework.DataAccess.Repositories.Interfaces;
using LotoHomework.Domain.Models;
using LotoHomework.Services.Interfaces;

namespace LotoHomework.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<Session> GetLatest()
        {
            return await _sessionRepository.GetLatestActiveAsync();
        }

        public async Task<bool> StartSession()
        {
            Session session = await _sessionRepository.GetLatestActiveAsync();
            if (session == null)
            {
                return false;
            }
            Session s = new Session
            {
                StartTime = DateTime.Now,
                IsEnded = false,
            };
            await _sessionRepository.InsertAsync(s);
            return true;
        }
    }
}
