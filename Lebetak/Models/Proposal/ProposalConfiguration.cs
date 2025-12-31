using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);
            

            // Relationships :
            // 1) Many-to-One: Proposal -> Worker
            builder
                .HasOne(p => p.Worker)
                .WithMany(w => w.Proposals)
                .HasForeignKey(p => p.WorkerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Post)
                .WithMany(e => e.Proposals)
                .HasForeignKey(e => e.PostId);
        }
    }
}
