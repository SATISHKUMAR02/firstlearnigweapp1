namespace WebApplication1.Data
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string  password { get; set; }

        public string  passwordSalt { get; set; }

        public int UserTypeId { get; set; }

        public bool isActive { get; set; }

        public bool  isDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DeletedDate { get; set; }

        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }

        //public virtual ICollection<UserType> UserTypes { get; set; }
         public virtual UserType UserType { get; set; }
        
    }
}
