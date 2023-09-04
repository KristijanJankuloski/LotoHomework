using System.ComponentModel.DataAnnotations;

namespace LotoHomework.DTOs.CombinationDTOs
{
    public class CombinationCreateDto
    {
        [Required]
        [MaxLength(7)]
        public int[] Numbers { get; set; }
    }
}
