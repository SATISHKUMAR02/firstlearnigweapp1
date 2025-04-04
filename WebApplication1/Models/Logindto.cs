using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Logindto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string Policy { get; set; }

    }
}
