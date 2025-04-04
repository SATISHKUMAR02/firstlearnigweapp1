using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace WebApplication1.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();// auto increment
            builder.Property(x => x.StudentName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(500);

            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    StudentName="Satish Kumar",
                    Address="Bangalore",
                    DOB= new DateTime(2022,1,01),
                    Email="sat@gmail.com"
                },
                new Student
                {
                    Id = 2,
                    StudentName="Kiran Kumar",
                    Address="Bangalore",
                    DOB= new DateTime(2012,1,1),
                    Email="kir@gmail.com"
                },
            }
            );
            builder.HasOne(n => n.Department)
                .WithMany(n => n.Students)
                .HasForeignKey(n => n.DepartmentId)
                .HasConstraintName("FK_Students_Department"); 
            // adding the foreign key config 
        
        }

    }
}
