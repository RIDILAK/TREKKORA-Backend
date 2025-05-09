﻿    using System;
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

            public DbSet<WishList> WishList { get; set; }

            public DbSet<Rating> Rating { get; set; }

            public DbSet<Booking> Bookings { get; set; }

        public DbSet<Message>Messages { get; set; }

        public DbSet<Notification> Notification { get; set; }

        public DbSet<Images> Images { get; set; }


            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                modelBuilder.Entity<States>()
              .HasOne(s => s.Countries)
               .WithMany(c => c.states)
               .HasForeignKey(s => s.CountryId)
               .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Place>()
                    .HasOne(p => p.State)
                    .WithMany(s => s.places)
                    .HasForeignKey(s => s.StateId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<GuideProfile>()
                    .HasOne(p => p.Place)
                    .WithMany(s => s.GuideProfiles)
                    .HasForeignKey(s => s.PlaceId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Countries>()
                    .HasIndex(c => c.CountryCode)
                    .IsUnique();

                modelBuilder.Entity<User>()
                    .HasIndex(u=>u.Email)
                    .IsUnique();

                modelBuilder.Entity<User>()
               .HasOne(u => u.GuideProfile)
               .WithOne(gp => gp.User)
               .HasForeignKey<GuideProfile>(gp => gp.GuideId)
               .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<WishList>()
                    .HasOne(w => w.User)
                    .WithMany(u => u.wishLists)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<WishList>()
                    .HasOne(w => w.Place)
                    .WithMany(p => p.WishList)
                    .HasForeignKey(p => p.PlaceId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<User>()
         .HasMany(r => r.Ratingss)
         .WithOne(u => u.User)
         .HasForeignKey(r => r.UserId)
         .OnDelete(DeleteBehavior.NoAction);
                ;

                modelBuilder.Entity<Rating>()
         .HasOne(r => r.Guide)
         .WithMany(u => u.Ratings)
         .HasForeignKey(r => r.GuideId)
         .OnDelete(DeleteBehavior.NoAction)
         .IsRequired(false);

                modelBuilder.Entity<Rating>()
                    .HasOne(r => r.Place)
                    .WithMany(u => u.Rating)
                    .HasForeignKey(r => r.PlaceId)
                     .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired(false);

                modelBuilder.Entity<Rating>()
                   .Property(r => r.RatingValue)
                   .HasColumnType("decimal(18,2)");

                modelBuilder.Entity<Place>()
                    .HasMany(t => t.Bookings)
                    .WithOne(p => p.place)
                    .HasForeignKey(t => t.PlaceId)
                    .OnDelete(DeleteBehavior.NoAction);


                modelBuilder.Entity<User>()
              .HasMany(u => u.Bookings)
              .WithOne(b => b.User)
              .HasForeignKey(b => b.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Place>()
                .HasOne(p => p.Images)
                .WithOne(i => i.Place)
                .HasForeignKey<Images>(i => i.PlaceId);

            }
    
        }
    }
