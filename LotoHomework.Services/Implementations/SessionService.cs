using LotoHomework.DataAccess.Repositories.Interfaces;
using LotoHomework.Domain.Models;
using LotoHomework.DTOs.SessionDTOs;
using LotoHomework.Mappers;
using LotoHomework.Services.Interfaces;

namespace LotoHomework.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ICombinationRepository _combinationRepository;
        public SessionService(ISessionRepository sessionRepository, ICombinationRepository combinationRepository)
        {
            _sessionRepository = sessionRepository;
            _combinationRepository = combinationRepository;

        }

        public async Task<Session> GetLatest()
        {
            return await _sessionRepository.GetLatestActiveAsync();
        }

        public async Task<SessionWinnersDto> GetWinners()
        {
            Session session = await _sessionRepository.GetLastEndedAsync();
            if (session == null)
            {
                return null;
            }
            return session.ToSessionWinners();
        }

        public async Task StartDraw(int adminId)
        {
            Session session = await _sessionRepository.GetLatestActiveAsync();
            if (session == null)
            {
                return;
            }

            DateTime endTime = DateTime.Now;

            Combination draw = new Combination { 
                UserId = adminId,
                EntryTime = endTime,
                SessionId = session.Id,
            };

            List<int> winningNumbers = new List<int>();
            while(winningNumbers.Count < 8)
            {
                Random random = new Random();
                int number = random.Next(1, 37);
                if(winningNumbers.Contains(number))
                {
                    continue;
                }
                winningNumbers.Add(number);
            }
            draw.SetNumbers(winningNumbers);
            session.WinningCombination = draw;

            foreach (Combination combination in session.EntryCombinations)
            {
                short matchAmmount = 0;
                foreach (int number in combination.GetNumbers())
                {
                    if (winningNumbers.Contains(number))
                        matchAmmount++;
                }

                if (matchAmmount >= 3)
                {
                    NumberMatch match = new NumberMatch { CombinationId = combination.Id, SessionId = session.Id, Ammount = matchAmmount };
                    session.NumberMatches.Add(match);
                }
            }
            session.EndTime = endTime;
            session.IsEnded = true;
            await _sessionRepository.UpdateAsync(session);
        }

        public async Task<bool> StartSession()
        {
            Session session = await _sessionRepository.GetLatestActiveAsync();
            if (session != null)
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
