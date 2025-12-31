using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    public class WorkerSkillsConfiguration : IEntityTypeConfiguration<WorkerSkills>
    {
        public void Configure(EntityTypeBuilder<WorkerSkills> builder)
        {
            builder.HasKey(e => new { e.SkillId, e.WorkerId });
            builder.HasOne(e => e.Worker)
                .WithMany(e => e.WorkerSkills)
                .HasForeignKey(e => e.WorkerId);
            builder.HasOne(e => e.Skill)
                .WithMany(e => e.WorkerSkills)
                .HasForeignKey(e => e.SkillId);

        }
    }
}
