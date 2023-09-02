using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotoHomework.Domain.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string Role { get; set; } = string.Empty;

        [InverseProperty("User")]
        public List<Combination> Combinations { get; set; }
    }
}
