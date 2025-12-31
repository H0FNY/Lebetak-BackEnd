using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class AttAnswerConfiguration : IEntityTypeConfiguration<AttAnswer>
    {
        public void Configure(EntityTypeBuilder<AttAnswer> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Answer)
                   .WithMany(a => a.attAnswers)
                   .HasForeignKey(e => e.AnswerId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
