using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMEN.Core.Entities;

namespace XMEN.Infrastructure.Data.Configurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.CreatedUTC)
            .HasColumnType("timestamp with time zone");

            builder.Property(e => e.UpdatedUTC)
            .HasColumnType("timestamp with time zone");
        }
    }
}
