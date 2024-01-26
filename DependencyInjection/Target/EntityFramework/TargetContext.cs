using DependencyInjection.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Target.EntityFramework
{
    public class TargetContext : DbContext
    {
        public TargetContext(DbContextOptions<TargetContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                   .OwnsOne(p => p.Price);
        }
    }
}
