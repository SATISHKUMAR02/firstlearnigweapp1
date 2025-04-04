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
