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
           
            base.OnModelCreating(builder);

            builder.Entity<Movies>()
                .HasMany(r => r.Reservations)
                .WithOne(m => m.Movie)
                .HasForeignKey(f => f.MovieId)
                .IsRequired();

            builder.Entity<Movies>()
                .HasMany(a => a.AvailableDate)
                .WithOne(r => r.Movie)
                .HasForeignKey(f => f.MovieId);


        }
    }
}