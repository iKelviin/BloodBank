using BloodBank.Services.BloodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Services.BloodStock.Infrastructure.Persistence;
    
public class BloodBankDbContext(DbContextOptions<BloodBankDbContext> options) : DbContext(options)
{
    public DbSet<Donation> Donations { get; set; }
    public DbSet<Donor> Donors { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<HealthPost> HealthPosts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Donor>(d =>
            {
                d.HasKey(k => k.Id);

                d.HasIndex(k => k.Email).IsUnique();

                d.HasMany(x => x.Donations)
                    .WithOne(o => o.Donor)
                    .HasForeignKey(o => o.DonorId)
                    .OnDelete(DeleteBehavior.Restrict);

                d.OwnsOne(x => x.Address, o =>
                {
                    o.Property(p => p.Street).HasColumnName("Street");
                    o.Property(p => p.City).HasColumnName("City");
                    o.Property(p => p.State).HasColumnName("State");
                    o.Property(p => p.ZipCode).HasColumnName("ZipCode");
                });
            });

        builder
            .Entity<Donation>(d =>
            {
                d.HasKey(k => k.Id);

                d.HasOne(o => o.Donor)
                    .WithMany(x => x.Donations)
                    .HasForeignKey(o => o.DonorId)
                    .OnDelete(DeleteBehavior.Restrict);

                d.HasOne(z => z.HealthPost)
                    .WithMany(x => x.Donations)
                    .HasForeignKey(z => z.HealthPostId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        builder
            .Entity<Stock>(s => { s.HasKey(k => k.Id); });

        builder
            .Entity<HealthPost>(s => { s.HasKey(k => k.Id); });

       

        base.OnModelCreating(builder);
    }
}