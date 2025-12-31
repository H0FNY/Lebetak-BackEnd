using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class QuestionConfigration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Text)
                .IsRequired()
                .HasMaxLength(1000);
            // Relationships :
            // 1) Many-to-One: Question
            builder
                .HasOne(q => q.Category)
                .WithMany(i => i.Questions)
                .HasForeignKey(q => q.CategoryId);
           
        }
    }
}
