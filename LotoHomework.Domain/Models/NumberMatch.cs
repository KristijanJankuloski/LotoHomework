using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotoHomework.Domain.Models
{
    public class NumberMatch : BaseEntity
    {
        public int SessionId { get; set; }
        [ForeignKey("SessionId")]
        public Session Session { get; set; }
        public int CombinationId { get; set; }
        [ForeignKey("CombinationId")]
        public Combination Combination { get; set; }
        [Range(1, 7)]
        public short Ammount { get; set; }
    }
}
