using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Lebetak.Models
{
    public class ServiceAnswerConfiguration : IEntityTypeConfiguration<ServiceAnswer>
    {
        public void Configure(EntityTypeBuilder<ServiceAnswer> builder)
        {
            builder.HasKey(sa => sa.Id);
            builder.HasOne(sa => sa.Service)
                   .WithMany(s => s.ServiceAnswers)
                   .HasForeignKey(sa => sa.ServiceId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(sa => sa.Question)
        .WithMany()
        .HasForeignKey(sa => sa.QuestionId)
        .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(sa => sa.Option)
                .WithMany()
                .HasForeignKey(sa => sa.OptionId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasIndex(sa => new { sa.ServiceId, sa.QuestionId }).IsUnique();

        }
    }
}
