using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class CompanyConfigration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Logo)
                .HasMaxLength(100);
            builder.Property(c => c.Panner)
                .HasMaxLength(100);
            builder.Property(c => c.Description)
                .HasMaxLength(1000);
            // Relationships :
            // 1) Many-to-One: Company -> Owner
            builder
                .HasOne(c => c.Category)
                .WithMany(o => o.Companies)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Owner)
                .WithOne(u => u.Company)
                .HasForeignKey<Company>(o => o.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
