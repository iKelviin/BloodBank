using BloodBank.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence;

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

        builder.Entity<HealthPost>().HasData(
            // Carapicuíba
            new HealthPost(
                id: Guid.Parse("c1a2b3c4-d5e6-f7a8-b9c0-d1e2f3a4b5c6"),
                name: "UBS Vila Dirce",
                address: "Rua São João, 120",
                city: "Carapicuíba"
            ),
            new HealthPost(
                id: Guid.Parse("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"),
                name: "UPA Carapicuíba",
                address: "Av. Deputado Emílio Carlos, 500",
                city: "Carapicuíba"
            ),
            new HealthPost(
                id: Guid.Parse("e7f8a9b0-c1d2-e3f4-a5b6-c7d8e9f0a1b2"),
                name: "UBS Inac",
                address: "Rua Amazonas, 45",
                city: "Carapicuíba"
            ),

            // Barueri
            new HealthPost(
                id: Guid.Parse("f1a2b3c4-d5e6-7f8a-9b0c-d1e2f3a4b5c6"),
                name: "Hospital Municipal de Barueri",
                address: "Rua Ângela Mirella, 354",
                city: "Barueri"
            ),
            new HealthPost(
                id: Guid.Parse("a4b5c6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d"),
                name: "UPA Barueri",
                address: "Av. Vinte e Seis de Março, 800",
                city: "Barueri"
            ),
            new HealthPost(
                id: Guid.Parse("b8c9d0e1-f2a3-b4c5-d6e7-f8a9b0c1d2e3"),
                name: "UBS Jardim Belval",
                address: "Rua Itajubá, 220",
                city: "Barueri"
            ),

            // Osasco
            new HealthPost(
                id: Guid.Parse("c1d2e3f4-a5b6-c7d8-e9f0-a1b2c3d4e5f6"),
                name: "Hospital Antonio Giglio",
                address: "Rua da Estação, 200",
                city: "Osasco"
            ),
            new HealthPost(
                id: Guid.Parse("d7e8f9a0-b1c2-d3e4-f5a6-b7c8d9e0f1a2"),
                name: "UBS Santo Antônio",
                address: "Av. Sport Club Corinthians Paulista, 345",
                city: "Osasco"
            ),
            new HealthPost(
                id: Guid.Parse("e9f0a1b2-c3d4-e5f6-a7b8-c9d0e1f2a3b4"),
                name: "UPA Conceição",
                address: "Rua Pedro Fioretti, 1123",
                city: "Osasco"
            ),

            // Cotia
            new HealthPost(
                id: Guid.Parse("f5a6b7c8-d9e0-f1a2-b3c4-d5e6f7a8b9c0"),
                name: "Hospital Regional de Cotia",
                address: "Rua Esmeralda, 85",
                city: "Cotia"
            ),
            new HealthPost(
                id: Guid.Parse("a1b2c3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6"),
                name: "UBS Portão",
                address: "Av. Lions Internacional, 1250",
                city: "Cotia"
            ),
            new HealthPost(
                id: Guid.Parse("b3c4d5e6-f7a8-b9c0-d1e2-f3a4b5c6d7e8"),
                name: "UPA Atalaia",
                address: "Estr. Morro Grande, 745",
                city: "Cotia"
            ),

            // Jandira
            new HealthPost(
                id: Guid.Parse("c7d8e9f0-a1b2-c3d4-e5f6-a7b8c9d0e1f2"),
                name: "Hospital Municipal de Jandira",
                address: "Rua Carmem, 112",
                city: "Jandira"
            ),
            new HealthPost(
                id: Guid.Parse("d9e0f1a2-b3c4-d5e6-f7a8-b9c0d1e2f3a4"),
                name: "UBS Jardim Gabriela",
                address: "Av. Presidente Costa e Silva, 250",
                city: "Jandira"
            ),
            new HealthPost(
                id: Guid.Parse("e1f2a3b4-c5d6-e7f8-a9b0-c1d2e3f4a5b6"),
                name: "UPA Jandira",
                address: "Rua São Roque, 1200",
                city: "Jandira"
            ),

            // Itapevi
            new HealthPost(
                id: Guid.Parse("f3a4b5c6-d7e8-f9a0-b1c2-d3e4f5a6b7c8"),
                name: "Hospital Geral de Itapevi",
                address: "Rua Benedito de Lima, 250",
                city: "Itapevi"
            ),
            new HealthPost(
                id: Guid.Parse("a7b8c9d0-e1f2-a3b4-c5d6-e7f8a9b0c1d2"),
                name: "UBS Jardim Briquet",
                address: "Av. Rubens Caramez, 987",
                city: "Itapevi"
            ),
            new HealthPost(
                id: Guid.Parse("b9c0d1e2-f3a4-b5c6-d7e8-f9a0b1c2d3e4"),
                name: "UPA Itapevi",
                address: "Rua Cesário Verde, 65",
                city: "Itapevi"
            ),

            // Santana de Parnaíba
            new HealthPost(
                id: Guid.Parse("c5d6e7f8-a9b0-c1d2-e3f4-a5b6c7d8e9f0"),
                name: "Hospital Santa Ana",
                address: "Rua Almirante Tamandaré, 150",
                city: "Santana de Parnaíba"
            ),
            new HealthPost(
                id: Guid.Parse("d7e8f9a0-b1c2-d3e4-f5a6-b7c8d9e0f1a2"),
                name: "UBS Fazendinha",
                address: "Av. Tenente Marques, 980",
                city: "Santana de Parnaíba"
            ),
            new HealthPost(
                id: Guid.Parse("e9f0a1b2-c3d4-e5f6-a7b8-c9d0e1f2a3b4"),
                name: "UPA Alphaville",
                address: "Alameda Rio Negro, 999",
                city: "Santana de Parnaíba"
            )
        );

        base.OnModelCreating(builder);
    }
}