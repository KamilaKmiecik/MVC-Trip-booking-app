using Microsoft.EntityFrameworkCore;
using UBB_Trips.Models;

namespace UBB_Trips.Data;

public class TripContext : DbContext
{
    public TripContext(DbContextOptions<TripContext> options) : base(options)
    {
    }

    public DbSet<Trip> Trips { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>().ToTable("Trip");
        modelBuilder.Entity<Client>().ToTable("Client");
        modelBuilder.Entity<Booking>().ToTable("Booking");
    }
}
