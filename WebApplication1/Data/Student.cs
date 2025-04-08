using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    public class Student
    {
        //[Key] // this acts as a primary key
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto generated 
        
        public int Id { get; set; } 
        
        [Required] // mentioning that it is required
        public string StudentName { get; set; }
        [EmailAddress] // accepts only proper email Address
        public string Email { get; set; }

        public string Address { get; set; }
          
        public DateTime DOB { get; set; }

        public int? DepartmentId { get; set; } // this is the foreign key and it 

        public virtual Department? Department { get; set; }
    }
}
