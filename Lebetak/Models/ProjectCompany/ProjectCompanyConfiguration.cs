using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class ProjectCompanyConfiguration : IEntityTypeConfiguration<ProjectCompany>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProjectCompany> builder)
        {
            builder.HasKey(pw => pw.Id);
            builder.Property(pw => pw.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(pw => pw.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder
                .HasOne(pw => pw.Company)
                .WithMany(w => w.Projects)
                .HasForeignKey(pw => pw.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
