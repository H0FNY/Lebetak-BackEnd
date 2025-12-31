using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class OwnerConfigration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(o => o.UserID);

            builder.HasOne(o => o.User)
                .WithOne(u => u.Owner)
                .HasForeignKey<Owner>(o => o.UserID)
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
