using Lebetak.Common.Enumes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(w => w.BudgetFrom)
           .HasPrecision(18, 4);
            builder.Property(w => w.BudgetTo)
           .HasPrecision(18, 4);

            builder.HasIndex(p => p.CreatedDate);

            builder.HasOne(p => p.Client)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(p => p.PostStatus)
            //    .WithMany(ps => ps.Posts)
            //    .HasForeignKey(p => p.PostStatusId)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Urgency)
                .WithMany(ps => ps.Posts)
                .HasForeignKey(p => p.UrgencyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);
            builder.Property(p => p.Location).IsRequired().HasMaxLength(200);
            builder.Property(p => p.CreatedDate).HasMaxLength(30);
            builder.Property(p => p.Status).HasConversion<int>().HasDefaultValue(JobStatus.Open);


        }
    }
}
