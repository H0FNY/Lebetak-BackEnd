using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models.Attachments.Att_Worker_Project
{
    public class AttWorkerProjectConfiguration : IEntityTypeConfiguration<AttWorkerProject>
    {
        public void Configure(EntityTypeBuilder<AttWorkerProject> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(e => e.Project)
                .WithMany(e => e.Images)
                .HasForeignKey(e => e.ProjectId);
        }
    }
}
