using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    public class UrgencyConfiguratiion : IEntityTypeConfiguration<Urgency>
    {
        public void Configure(EntityTypeBuilder<Urgency> builder)
        {
            builder.HasKey(u => u.Id);
        }
    }
}
