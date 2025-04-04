﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Config;

namespace WebApplication1.Data
{
    public class CollegeDbContext : DbContext
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