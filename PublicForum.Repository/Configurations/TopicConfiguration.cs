using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Repository.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(c => c.Description)
                .HasMaxLength(10000)
                .IsRequired();

            builder.Property(c => c.Title)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.Created)
                .IsRequired();

            builder.Property(c => c.OwnerId)
              .HasMaxLength(250)
              .IsRequired();

            builder.Property(c => c.IsDeleted)
               .HasDefaultValue(false)
               .IsRequired();

        }
    }
}
