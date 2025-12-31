using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class AttProposalConfiguration : IEntityTypeConfiguration<Att_Proposal>
    {
        public void Configure(EntityTypeBuilder<Att_Proposal> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Proposal)
                .WithMany(e => e.Images)
                .HasForeignKey(e => e.ProposalId);
        }
    }
}
