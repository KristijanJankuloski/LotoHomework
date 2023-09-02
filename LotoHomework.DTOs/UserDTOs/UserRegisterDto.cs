using System.ComponentModel.DataAnnotations;

namespace LotoHomework.DTOs.UserDTOs
{
    public class UserRegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string FullName { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
