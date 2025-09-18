using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Kanban.Domain.Models.Task;

namespace Kanban.DatabaseContext.Configurations
{
    public class TaskConfigurations : EntityTypeBaseConfiguration<Task>
    {
        protected override void ConfigurateConstraints(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(t => t.Id);

            // Relación: Task pertenece a un Board
            builder.HasOne(t => t.Board)
                   .WithMany(b => b.Tasks)
                   .HasForeignKey(t => t.BoardId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relación: Task puede estar asignada a un User
            builder.HasOne(t => t.AssignedUser)
                   .WithMany(u => u.Tasks)
                   .HasForeignKey(t => t.AssignedUserId)
                   .OnDelete(DeleteBehavior.SetNull);
        }

        protected override void ConfigurateProperties(EntityTypeBuilder<Task> builder)
        {
            builder.Property(t => t.Title)
          .IsRequired()
          .HasMaxLength(200);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            builder.Property(t => t.Status)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(t => t.Priority)
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .IsRequired();

            builder.Property(t => t.UpdatedAt)
                   .IsRequired(false);

            builder.Property(t => t.DueDate)
                   .IsRequired(false);
        }

        protected override void ConfigurateTableName(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Tasks");
        }
    }
}
