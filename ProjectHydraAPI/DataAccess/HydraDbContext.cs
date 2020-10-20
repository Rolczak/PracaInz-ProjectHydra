using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectHydraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.DataAccess
{
    public class HydraDbContext : IdentityDbContext<AppUser>
    {
        public HydraDbContext(DbContextOptions options) : base(options)
        {         
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Unit>()
                .HasOne<Unit>(s => s.Parent)
                .WithMany(s => s.ChildernUnits)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Unit>()
                .HasMany<AppUser>(s => s.SoldiersInUnit)
                .WithOne(u => u.Unit)
                .HasForeignKey(u => u.UnitId);
        }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Class> Classes { get; set; }
    }


}
