using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicForum.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Repository.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(c => c.FirstName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
