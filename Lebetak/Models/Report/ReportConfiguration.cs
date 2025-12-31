using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.HasOne(e => e.Client)
                .WithMany(e => e.Reports)
                .HasForeignKey(e => e.ClientId);

            builder.HasOne(e => e.Worker)
                .WithMany(e => e.Reports)
                .HasForeignKey(e => e.WorkerId);

        }
    }
}
