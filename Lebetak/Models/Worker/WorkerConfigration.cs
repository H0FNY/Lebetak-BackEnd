using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    public class WorkerConfigration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasKey(w=>w.UserId);
            builder.Property(w => w.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(w => w.Is_Online)
                .HasDefaultValue(false);
            builder.Property(w => w.Is_Booked)
                .HasDefaultValue(false);
            builder.Property(w => w.Is_Avilable)
                .HasDefaultValue(true);


            // Relationships :
            builder.HasOne(c => c.User)
                   .WithOne(w =>w.Worker)
                   .HasForeignKey<Worker>(c => c.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
            
            // 3) Many-to-Many: Worker <-> ProjectWorker
            builder
                .HasMany(w => w.Projects)
                .WithOne(p=>p.Worker)
                .HasForeignKey(p => p.WorkerId)
                .OnDelete(DeleteBehavior.NoAction);


            //
            builder.HasOne(e => e.Category)
                   .WithMany(e => e.Workers)
                   .HasForeignKey(e => e.CategoryId);
        }
    }
}
