using Kanban.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.DatabaseContext.Configurations
{
    public class BoardConfigurations : EntityTypeBaseConfiguration<Board>
    {
        protected override void ConfigurateConstraints(EntityTypeBuilder<Board> builder)
        {
            builder.HasKey(b => b.Id);

            // Relación: un Board pertenece a un Owner (User)
            builder.HasOne(b => b.Owner)
                   .WithMany(u => u.Boards)
                   .HasForeignKey(b => b.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relación: un Board tiene muchas Columns
            builder.HasMany(b => b.Columns)
                   .WithOne(c => c.Board)
                   .HasForeignKey(c => c.BoardId);

            // Relación opcional: un Board puede tener muchas Tasks
            builder.HasMany(b => b.Tasks)
                   .WithOne(t => t.Board)
                   .HasForeignKey(t => t.BoardId);
        }

        protected override void ConfigurateProperties(EntityTypeBuilder<Board> builder)
        {
            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(b => b.Description)
                   .HasMaxLength(1000);

            builder.Property(b => b.CreatedAt)
                   .IsRequired();

            builder.Property(b => b.UpdatedAt)
                   .IsRequired(false);
        }

        protected override void ConfigurateTableName(EntityTypeBuilder<Board> builder)
        {
            builder.ToTable("Boards");
        }
    }
}
