using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    public class ComponyPhonesConfiguration : IEntityTypeConfiguration<ComponyPhones>
    {
        public void Configure(EntityTypeBuilder<ComponyPhones> builder)
        {
            builder.HasKey(cp => new { cp.CompanyId, cp.PhoneNumber }); 
            builder.Property(cp => cp.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            // Relationships :
            // 1) Many-to-One: ComponyPhones -> Company
            builder
                .HasOne(cp => cp.Company)
                .WithMany(c => c.ComponyPhones)
                .HasForeignKey(cp => cp.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
