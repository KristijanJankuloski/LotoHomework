using LotoHomework.Domain.Models;
using LotoHomework.DTOs.CombinationDTOs;
using LotoHomework.DTOs.SessionDTOs;

namespace LotoHomework.Mappers
{
    public static class SessionMappers
    {
        public static SessionWinnersDto ToSessionWinners(this Session session)
        {
            var winners = new SessionWinnersDto
            {
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Id = session.Id,
                WinningNumbers = session.WinningCombination.GetNumbers(),
            };
            winners.Matches = new List<MatchListDto>();
            foreach(var match in session.NumberMatches)
            {
                var matchDto = new MatchListDto
                {
                    Ammount = match.Ammount,
                    Numbers = match.Combination.GetNumbers(),
                    UserId = match.Combination.UserId,
                    FullName = match.Combination.User.FullName,
                    Prize = match.Ammount switch
                    {
                        3 => "50$ Gift Card",
                        4 => "100$ Gift Card",
                        5 => "TV",
                        6 => "Vacation",
                        7 => "JackPot",
                        _ => "/"
                    }
                };
                winners.Matches.Add(matchDto);
            }
            return winners;
        }
    }
}
