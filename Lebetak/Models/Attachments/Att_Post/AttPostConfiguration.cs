using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    public class AttPostConfiguration : IEntityTypeConfiguration<AttPost>
    {
        public void Configure(EntityTypeBuilder<AttPost> builder)
        {
            builder.HasKey(ap => ap.Id);
            builder.Property(ap => ap.URL).IsRequired().HasMaxLength(200);
            builder.HasOne(ap => ap.Post)
                   .WithMany(p => p.AttPosts)
                   .HasForeignKey(ap => ap.PostId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
