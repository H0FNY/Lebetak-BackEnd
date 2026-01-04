using Lebetak.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ChatNotificationConfiguration : IEntityTypeConfiguration<ChatNotification>
{
    public void Configure(EntityTypeBuilder<ChatNotification> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Message).IsRequired().HasMaxLength(500);
        builder.Property(n => n.IsRead).HasDefaultValue(false);
        builder.HasIndex(n => n.CreatedAt);
    }
}
