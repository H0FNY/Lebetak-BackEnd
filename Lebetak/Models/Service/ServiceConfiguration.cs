using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Description).IsRequired().HasMaxLength(200);
            builder.Property(s => s.CreatedDate).HasMaxLength(50);
            builder.Property(s => s.PreferredTime).HasMaxLength(50);
            builder.HasOne(s => s.Client)
                .WithMany(o => o.Services)
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
