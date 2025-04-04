using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data.Config
{
    public class UserTypeConfig : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();// auto increment
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);

            builder.HasData(new List<UserType>()
            {
                new UserType
                {
                    Id=1,
                    Name="Student",
                    Description="for Students"
                },new UserType
                {
                    Id=2,
                    Name="Faculty",
                    Description="for Faculties"
                },new UserType
                {
                    Id=3,
                    Name="Supporting Staff",
                    Description="for Supporting Staff"
                },new UserType
                {
                    Id=4,
                    Name="Parents",
                    Description="for Parents"
                },
            });

           
        }

    }
}