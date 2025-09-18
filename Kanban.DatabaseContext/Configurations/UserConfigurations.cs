using Kanban.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.DatabaseContext.Configurations
{
    public class UserConfigurations : EntityTypeBaseConfiguration<User>
    {
        protected override void ConfigurateConstraints(EntityTypeBuilder<User> builder)
        {
            // Clave primaria
            builder.HasKey(u => u.Id);

            // Índices únicos
            builder.HasIndex(u => u.Email).IsUnique();

            // Relaciones
            builder.HasMany(u => u.Boards)
                   .WithOne(b => b.Owner)
                   .HasForeignKey(b => b.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Tasks)
                   .WithOne(t => t.AssignedUser)
                   .HasForeignKey(t => t.AssignedUserId)
                   .OnDelete(DeleteBehavior.SetNull);
        }

        protected override void ConfigurateProperties(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnType("varchar(200)");

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)");

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            builder.Property(u => u.PasswordSalt)
                   .IsRequired();

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(26)
                   .HasColumnType("varchar(26)");

            builder.Property(u => u.Country)
                   .HasMaxLength(30)
                   .HasColumnType("varchar(30)");

            builder.Property(u => u.City)
                   .HasMaxLength(30)
                   .HasColumnType("varchar(30)");
        }

        protected override void ConfigurateTableName(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
        }
    }
}
