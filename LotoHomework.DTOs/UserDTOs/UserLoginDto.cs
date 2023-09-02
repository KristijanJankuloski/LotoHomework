using System.ComponentModel.DataAnnotations;

namespace LotoHomework.DTOs.UserDTOs
{
    public class UserLoginDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
