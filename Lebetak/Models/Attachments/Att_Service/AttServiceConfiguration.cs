using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class AttServiceConfiguration : IEntityTypeConfiguration<AttService>
    {
        public void Configure(EntityTypeBuilder<AttService> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(ap => ap.Service)
                   .WithMany(p => p.AttService)
                   .HasForeignKey(ap => ap.ServiceId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
