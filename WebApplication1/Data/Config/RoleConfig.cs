using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Config
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();// auto increment
            builder.Property(x => x.RoleName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
           // builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.isDeleted).IsRequired();
            builder.Property(x => x.isActive).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            
        }

    }
    
}
