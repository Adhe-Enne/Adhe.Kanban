using Kanban.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.DatabaseContext.Configurations
{
    public class ColumnConfigurations : EntityTypeBaseConfiguration<Column>
    {
        protected override void ConfigurateConstraints(EntityTypeBuilder<Column> builder)
        {
            builder.HasKey(c => c.Id);

            // Relación: una Column pertenece a un Board
            builder.HasOne(c => c.Board)
                   .WithMany(b => b.Columns)
                   .HasForeignKey(c => c.BoardId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relación: una Column tiene muchas Tasks
            builder.HasMany(c => c.Tasks)
                   .WithOne(t => t.Column)
                   .HasForeignKey(t => t.ColumnId);
        }

        protected override void ConfigurateProperties(EntityTypeBuilder<Column> builder)
        {
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnType("varchar(200)");

            builder.Property(c => c.Order)
                   .IsRequired();
        }

        protected override void ConfigurateTableName(EntityTypeBuilder<Column> builder)
        {
            builder.ToTable("Columns");
        }
    }
}
