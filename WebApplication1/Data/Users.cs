namespace WebApplication1.Data
{
    public class User
    {
        // to never edit an attribute -> [NeverValidate]
        public int Id { get; set; }
        [Required(ErrorMessage  = "username is required")]
        [StringLength(30)] // mentions the max length of characters
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

        // public string Password {get;set;}
        //[Compare(nameof(Password)] these are the ways to validate with the password and the confirm password
        // public string confirmPassword {get;set;}



        /* there are different validators present in the .NET , which are inbuilt 
        [CreditCard] => validates the property of credit card
        [Range]
        [RegularExpression]
        [Url]
        */
        
    }
}
