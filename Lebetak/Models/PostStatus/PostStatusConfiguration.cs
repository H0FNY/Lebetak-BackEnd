using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    public class PostStatusConfiguration : IEntityTypeConfiguration<PostStatus>
    {
        public void Configure(EntityTypeBuilder<PostStatus> builder)
        {
            builder.HasKey(ps => ps.Id);
            builder.Property(ps => ps.Title).IsRequired().HasMaxLength(20);
        }
    }
}
