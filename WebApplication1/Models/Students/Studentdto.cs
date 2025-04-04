using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Students
{
    public class Studentdto
    {
        public int Id { get; set; }
        [Required]

        public string StudentName { get; set; }
        [Required]

        public string email { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime DOB { get; set; }
    }
}
