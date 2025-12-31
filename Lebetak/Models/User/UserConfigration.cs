using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lebetak.Models
{
    public class UserConfigration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasOne(p => p.Wallet)
                   .WithOne(c => c.User)
                   .HasForeignKey<Wallet>(p => p.UserId);
        }
    }
}
