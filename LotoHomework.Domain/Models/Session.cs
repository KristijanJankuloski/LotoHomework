using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotoHomework.Domain.Models
{
    public class Session : BaseEntity
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [InverseProperty("Session")]
        public List<Combination> EntryCombinations { get; set; } = new();

        public bool IsEnded { get; set; }

        public int? CombinationId { get; set; }

        [ForeignKey("CombinationId")]
        public Combination? WinningCombination { get; set; }

        [InverseProperty("Session")]
        public List<NumberMatch> NumberMatches { get; set; } = new();
    }
}
