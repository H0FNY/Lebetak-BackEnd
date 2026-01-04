using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    public class ProposalNotificationConfigration : IEntityTypeConfiguration<ProposalNotification>
    {

        public void Configure(EntityTypeBuilder<ProposalNotification> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Message).IsRequired().HasMaxLength(500);
            builder.Property(n => n.IsRead).HasDefaultValue(false);
            builder.HasIndex(n => n.CreatedAt);
        }
    }

}