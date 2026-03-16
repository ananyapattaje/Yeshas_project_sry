using Microsoft.EntityFrameworkCore;
using CAPGEMINI_CROPDEAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CAPGEMINI_CROPDEAL.Data;

public class CropDealDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public CropDealDbContext(DbContextOptions<CropDealDbContext> options)
        : base(options)
    {
    }

    public DbSet<Farmer> Farmers { get; set; }
    public DbSet<Crop> Crops { get; set; }
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<CropSubscription> CropSubscriptions { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Farmer)
            .WithMany()
            .HasForeignKey(o => o.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Buyer)
            .WithMany()
            .HasForeignKey(o => o.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Crop)
            .WithMany()
            .HasForeignKey(o => o.CropId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}