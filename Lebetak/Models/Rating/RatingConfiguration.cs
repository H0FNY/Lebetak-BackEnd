using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Proposal)
                .WithOne(e => e.Rating)
                .HasForeignKey<Rating>(e => e.ProposalId);
        }
    }
}
