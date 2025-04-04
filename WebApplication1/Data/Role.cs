namespace WebApplication1.Data
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public bool isActive { get; set; }

        public bool isDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; } 

        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } 



    }
}
