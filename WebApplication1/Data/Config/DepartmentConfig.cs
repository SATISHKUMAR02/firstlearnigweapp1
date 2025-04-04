using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();// auto increment
            builder.Property(x => x.DepartmentName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.description).IsRequired(false).HasMaxLength(500);

            builder.HasData(new List<Department>()
            {
                new Department
                {
                    Id = 1,
                    DepartmentName="ECE",
                    description="electronics",
                  
                },
                new Department
                {
                    Id = 2,
                    DepartmentName="CSE",
                    description="Computer",
                    
                },
            }
            );

        }

    }
}
