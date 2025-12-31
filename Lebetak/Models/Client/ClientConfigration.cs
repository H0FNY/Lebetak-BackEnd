using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class ClientConfigration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.UserId);
            builder.HasOne(c => c.User)
                   .WithOne(w=>w.Client)
                   .HasForeignKey<Client>(c => c.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
