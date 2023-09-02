using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LotoHomework.Domain.Models
{
    public class Combination : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int SessionId { get; set; }

        [ForeignKey("SessionId")]
        public Session Session { get; set; }

        public DateTime EntryTime { get; set; }

        [Required]
        [MaxLength(24)]
        public string Numbers { get; set; } = string.Empty;


        public void SetNumbers(List<int> numbers)
        {
            if (numbers.Count > 8 || numbers.Count < 7)
                throw new ArgumentException("Numbers must either 7");

            numbers.Sort();

            string numberString = "";
            for(int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] < 1 || numbers[i] > 37)
                    throw new ArgumentException("Numbers have incorrect values");

                if(i ==  numbers.Count - 1)
                {
                    numberString += numbers[i].ToString();
                    break;
                }
                numberString += $"{numbers[i]},";
            }
            Numbers = numberString;
        }

        public List<int> GetNumbers()
        {
            List<string> numberStrings = Numbers.Split(',').ToList();
            return numberStrings.Select(x => Convert.ToInt32(x)).ToList();
        }
    }
}
