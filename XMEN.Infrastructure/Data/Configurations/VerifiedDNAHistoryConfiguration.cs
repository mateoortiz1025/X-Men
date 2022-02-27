using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMEN.Core.Entities;
using XMEN.Core.Enumerations;

namespace XMEN.Infrastructure.Data.Configurations
{
    public class VerifiedDNAHistoryConfiguration : BaseEntityConfiguration<VerifiedDNAHistory>
    {
        public override void Configure(EntityTypeBuilder<VerifiedDNAHistory> builder)
        {
            builder.ToTable("VerifiedDNAHistory");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.DNA)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.HumanType)
                   .HasConversion(x => (int)x, x => (HumanType)x);
        }
    }
}
