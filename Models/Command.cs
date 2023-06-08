using System.ComponentModel.DataAnnotations;

namespace CommandsApi.Models
{
    public class Command
    {
        public int Id { get; set; }
        [StringLength(500)]
        [Required]
        public string HowTo { get; set; }

        [Required(ErrorMessage = "Line is Required")]
        [StringLength(100)]
        public string Line { get; set; }

        [StringLength(100)]
        public string Platform { get; set; }    

    }
}
