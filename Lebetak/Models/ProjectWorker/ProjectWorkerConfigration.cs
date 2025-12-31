using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class ProjectWorkerConfigration : IEntityTypeConfiguration<ProjectWorker>
    {
        public void Configure(EntityTypeBuilder<ProjectWorker> builder)
        {
            builder.HasKey(pw => pw.Id);
            builder.Property(pw => pw.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(pw => pw.Description)
                .IsRequired()
                .HasMaxLength(1000);

            // Relationships :
            // 1) Many-to-One: ProjectWorker -> Worker
            builder
                .HasOne(pw => pw.Worker)
                .WithMany(w => w.Projects)
                .HasForeignKey(pw => pw.WorkerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
