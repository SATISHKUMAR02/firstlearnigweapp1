using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Config
{
    public class UserConfig: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();// auto increment
            builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
            builder.Property(x => x.password).IsRequired().HasMaxLength(500);
            builder.Property(x => x.passwordSalt).IsRequired().HasMaxLength(500);
            builder.Property(x => x.isDeleted).IsRequired();
            builder.Property(x => x.isActive).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UserTypeId).IsRequired();

            builder.HasOne(n => n.UserType)
               .WithMany(n => n.Users)
               .HasForeignKey(n => n.UserTypeId)
               .HasConstraintName("FK_UserType_User");

        }

    }
    

    
}
