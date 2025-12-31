using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class OptionConfigration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Text)
                .IsRequired()
                .HasMaxLength(500);

            builder
               .HasOne(q => q.Question)
               .WithMany(c => c.options)
               .HasForeignKey(q => q.QuestionId);
        }
    }
}
