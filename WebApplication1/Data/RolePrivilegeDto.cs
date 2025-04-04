using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class RolePrivilegeDto
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        [Required]
        public string RolePrivilegeName { get; set; }

        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
