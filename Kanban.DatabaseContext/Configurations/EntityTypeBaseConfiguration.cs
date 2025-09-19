using Kanban.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.DatabaseContext.Configurations
{
    public abstract class EntityTypeBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseModel
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            // Usar tipo timestamp para compatibilidad con MySQL y valores por defecto
            builder.Property(u => u.CreatedAt)
                   .HasColumnType("timestamp")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(u => u.UpdatedAt)
                   .HasColumnType("timestamp")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");

            ConfigurateProperties(builder);
            ConfigurateConstraints(builder);
            ConfigurateTableName(builder);
        }

        protected abstract void ConfigurateProperties(EntityTypeBuilder<T> builder);
        protected abstract void ConfigurateConstraints(EntityTypeBuilder<T> builder);
        protected abstract void ConfigurateTableName(EntityTypeBuilder<T> builder);
    }
}
