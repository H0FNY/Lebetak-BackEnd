using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class AttCompanyProjectConfiguration : IEntityTypeConfiguration<AttCompanyProject>
    {
        public void Configure(EntityTypeBuilder<AttCompanyProject> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(e => e.Project)
                .WithMany(e => e.Images)
                .HasForeignKey(e => e.ProjectId);
        }
    }
}
