using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Config;

namespace WebApplication1.Data
{ 

    // this class is basically like a Database in the Entity Framework
    public class CollegeDbContext : DbContext // this is the main class from which we are inheriting
    
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options ):base(options) 
        {
            
        }
        public DbSet<Student> Students {  get; set; } 

        public DbSet<Department> Departments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }

        public DbSet<RolePrivilege> RolePrivileges { get; set; }

        public DbSet<UserType> UserTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) // this method is used to provide configurations for the tables mentioned 
        {
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new RolePrivilegeConfig());
            modelBuilder.ApplyConfiguration(new UserRoleMappingConfig());
            modelBuilder.ApplyConfiguration(new UserTypeConfig());

            //modelBuilder.ApplyConfiguration(new AnotherTableConfig());
            // In case if we want to add new table , we can follow the same and add another line here
/* =============================================================================================================================================================important
            DbSet<student> Students -> Student is one of the tables which is present as a DbSet , it is stored as Students in .NET CORE 
            after bulding DbContext, we can start querying the database using LINQ queries
            

        
protected override void OnModelCreating(ModelBuilder modelbuilder){
            modelbuilder.Entity<Student>().hasData(new List<Student>(){
                new Student {

                this is how we can add some data from the code first approach
                }
            
            }
    }


*/





            //modelBuilder.Entity<Student>(entity =>
            //{
            //    entity.Property(n => n.StudentName).IsRequired().HasMaxLength(250);

            //    entity.Property(n => n.Address).IsRequired().HasMaxLength(500);
            //    entity.Property(n => n.Email).IsRequired().HasMaxLength(400);
            // }
            //); Not an Ideal method to provide properties to the 
            // tables in the database , we might have to create the same for all 1000 tables 

        }

    }
}
// basically we are registering all the database tables in this file  

