using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder.ToTable("user");
        builder.HasKey(X => X.IdPk);
        builder.Property(x => x.IdPk)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)            
            .IsRequired()
            .HasColumnName("idPk")
            .HasColumnType("INT");

        builder.Property(x => x.Usename)
            .IsRequired()
            .HasColumnName("username")
            .HasMaxLength(50);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName("password")
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("email")
            .HasMaxLength(100);    

        builder.HasMany(x => x.Rols)
            .WithMany(x => x.Users)
            .UsingEntity<RolUser>(
                t => t.HasOne(j => j.Rol)
                    .WithMany(t => t.RolUsers)
                    .HasForeignKey(j => j.Rol),
                t => t.HasOne(j => j.User)
                    .WithMany(t => t.RolUsers)
                    .HasForeignKey(j => j.User),                
                t => {
                    t.Property(j => j.RolIdPk)
                        .HasColumnName("rolIdPk")
                        .IsRequired()
                        .HasColumnType("INT");
                    t.Property(j => j.UserIdPk)
                        .HasColumnName("userIdPk")
                        .IsRequired()
                        .HasColumnType("INT");                    
                    t.HasKey(j => new {j.RolIdPk, j.UserIdPk});
                }
            );
                
            
    }
}
