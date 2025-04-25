using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<GuideProfile> GuideProfiles { get; set; }

        public DbSet<Countries> Countries { get; set; }

        public DbSet<States> States { get; set; }

        public DbSet<Place> Places { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
           modelBuilder.Entity<States>()
         .HasOne(s => s.Countries)
          .WithMany(c => c.states)
          .HasForeignKey(s => s.CountryId)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Place>()
                .HasOne(p=>p.State)
                .WithMany(s=>s.places)
                .HasForeignKey(s => s.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
           .HasOne(u => u.GuideProfile)
           .WithOne(gp => gp.User)
           .HasForeignKey<GuideProfile>(gp => gp.GuideId)
           .OnDelete(DeleteBehavior.Cascade);


        }
    }

}
