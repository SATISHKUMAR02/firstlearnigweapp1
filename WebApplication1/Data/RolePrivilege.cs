namespace WebApplication1.Data
{
    public class RolePrivilege
    {
        public int Id { get; set; }

        public string RoleprivilegeName { get; set; }

        public string Description { get; set; }

        public int RoleId { get; set; }

        public bool isActive { get; set; }

        public bool isDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DeletedDate { get; set; }

        public Role Role { get; set; }




    }
}
