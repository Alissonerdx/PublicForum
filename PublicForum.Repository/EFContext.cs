using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PublicForum.Auth.Model;
using PublicForum.Domain.Entities;
using PublicForum.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Repository
{
    public class EFContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Topic> Topics { get; set; }
        public EFContext(DbContextOptions<EFContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TopicConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
