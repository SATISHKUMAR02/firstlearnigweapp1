namespace WebApplication1.Models
{
    public class UserDto
    {
        public int  Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int UserTypeId { get; set; }


    }
}
/*
Structure of EFC bacially an ORM
the Database server has a database , to use this database with entity framework , we need to create DbContext for each database ,
two database -> two DbContext 




*/
