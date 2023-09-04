using LotoHomework.DTOs.CombinationDTOs;

namespace LotoHomework.DTOs.SessionDTOs
{
    public class SessionWinnersDto
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<int> WinningNumbers { get; set; }
        public List<MatchListDto> Matches { get; set; }
    }
}
