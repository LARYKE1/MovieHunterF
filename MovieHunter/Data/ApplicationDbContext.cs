using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieHunter.Models;

namespace MovieHunter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<AvailableDate> AvailableDate { get; set; } 

        protected override void OnModelCreating (ModelBuilder builder)
        {
            builder.Entity<Movies>(entity =>
            {
                entity.HasKey(d => d.MovieId);
             });

            builder.Entity<Reservation>(entity =>
            {
                entity.HasOne(d => d.Movie)
                .WithMany(p => p.Reservations)
                .HasForeignKey(p => p.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Movie_1");
            });


            builder.Entity<AvailableDate>(entity =>
            {
                entity.HasKey(d => d.DateId);
                entity.HasOne(d => d.Movie)
                        .WithMany(a => a.AvailableDate)
                        .HasForeignKey(d => d.MovieId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Available_Movie");
            });

            base.OnModelCreating(builder);
        }
    }
}