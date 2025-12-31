using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Text)
                .IsRequired()
                .HasMaxLength(1000);
            //builder
            //   .HasOne(a => a.question)
            //   .WithOne(o => o.answer)
            //   .HasForeignKey<Answer>(a => a.QuestionId);
            
        }
    }
}
