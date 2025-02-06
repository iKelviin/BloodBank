using BloodBank.Services.Core.BloodStock.Entities;
using BloodBank.Services.Core.BloodStockEntities;
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

        builder.Entity<HealthPost>().HasData(
            // Carapicuíba
            new HealthPost(
                id: Guid.Parse("4e432145-cecb-4417-a3a3-f730d444d1b4"),
                name: "UBS Vila Dirce",
                address: "Rua São João, 120",
                city: "Carapicuíba"
            ),
            new HealthPost(
                id: Guid.Parse("18028a3e-f4ad-45be-b586-9c63c4627e12"),
                name: "UPA Carapicuíba",
                address: "Av. Deputado Emílio Carlos, 500",
                city: "Carapicuíba"
            ),
            new HealthPost(
                id: Guid.Parse("40457671-1052-4922-bda7-dc721e4833bd"),
                name: "UBS Inac",
                address: "Rua Amazonas, 45",
                city: "Carapicuíba"
            ),

// Barueri
            new HealthPost(
                id: Guid.Parse("cc7f40bf-0ba8-4a91-aeae-c772fbb965f1"),
                name: "Hospital Municipal de Barueri",
                address: "Rua Ângela Mirella, 354",
                city: "Barueri"
            ),
            new HealthPost(
                id: Guid.Parse("948e1a29-0a9b-4394-b748-6296cc7a1513"),
                name: "UPA Barueri",
                address: "Av. Vinte e Seis de Março, 800",
                city: "Barueri"
            ),
            new HealthPost(
                id: Guid.Parse("d5259026-0931-4fcb-bc98-63f0683ecbe2"),
                name: "UBS Jardim Belval",
                address: "Rua Itajubá, 220",
                city: "Barueri"
            ),

// Osasco
            new HealthPost(
                id: Guid.Parse("f39a504a-0b7e-44fc-908b-506d97d878c9"),
                name: "Hospital Antonio Giglio",
                address: "Rua da Estação, 200",
                city: "Osasco"
            ),
            new HealthPost(
                id: Guid.Parse("4a226750-903f-45e3-a1ec-5b76077ad3df"),
                name: "UBS Santo Antônio",
                address: "Av. Sport Club Corinthians Paulista, 345",
                city: "Osasco"
            ),
            new HealthPost(
                id: Guid.Parse("3804fb9d-f363-4b9e-86bf-9ae37e1a1142"),
                name: "UPA Conceição",
                address: "Rua Pedro Fioretti, 1123",
                city: "Osasco"
            ),

// Cotia
            new HealthPost(
                id: Guid.Parse("4aeed005-1117-490b-bf22-e5a24fd95e91"),
                name: "Hospital Regional de Cotia",
                address: "Rua Esmeralda, 85",
                city: "Cotia"
            ),
            new HealthPost(
                id: Guid.Parse("416921ca-7207-444c-a198-6ff2b0bf1bdf"),
                name: "UBS Portão",
                address: "Av. Lions Internacional, 1250",
                city: "Cotia"
            ),
            new HealthPost(
                id: Guid.Parse("bae4338d-aa25-48e6-9885-68c595a8fb12"),
                name: "UPA Atalaia",
                address: "Estr. Morro Grande, 745",
                city: "Cotia"
            ),

// Jandira
            new HealthPost(
                id: Guid.Parse("c06928e9-1545-44d3-827f-611cb493b037"),
                name: "Hospital Municipal de Jandira",
                address: "Rua Carmem, 112",
                city: "Jandira"
            ),
            new HealthPost(
                id: Guid.Parse("756d796f-9a24-4f93-a3ae-1da248bc4b8f"),
                name: "UBS Jardim Gabriela",
                address: "Av. Presidente Costa e Silva, 250",
                city: "Jandira"
            ),
            new HealthPost(
                id: Guid.Parse("47201fd2-7061-49a4-b183-a9fd312581bf"),
                name: "UPA Jandira",
                address: "Rua São Roque, 1200",
                city: "Jandira"
            ),

// Itapevi
            new HealthPost(
                id: Guid.Parse("0bfaf448-bd38-47d2-92c2-4668f99cd7f0"),
                name: "Hospital Geral de Itapevi",
                address: "Rua Benedito de Lima, 250",
                city: "Itapevi"
            ),
            new HealthPost(
                id: Guid.Parse("bc827713-406a-4325-a1e2-353911364a4f"),
                name: "UBS Jardim Briquet",
                address: "Av. Rubens Caramez, 987",
                city: "Itapevi"
            ),
            new HealthPost(
                id: Guid.Parse("eda86541-3ecb-4bf6-8ec0-771cf481699f"),
                name: "UPA Itapevi",
                address: "Rua Cesário Verde, 65",
                city: "Itapevi"
            ),

// Santana de Parnaíba
            new HealthPost(
                id: Guid.Parse("0a390b24-257c-484f-b930-1bc1c8e7ab8c"),
                name: "Hospital Santa Ana",
                address: "Rua Almirante Tamandaré, 150",
                city: "Santana de Parnaíba"
            ),
            new HealthPost(
                id: Guid.Parse("560da268-36fd-4708-9d58-6b38bb728560"),
                name: "UBS Fazendinha",
                address: "Av. Tenente Marques, 980",
                city: "Santana de Parnaíba"
            ),
            new HealthPost(
                id: Guid.Parse("a4e7b36a-c336-4dd2-b415-11c4a52e1f44"),
                name: "UPA Alphaville",
                address: "Alameda Rio Negro, 999",
                city: "Santana de Parnaíba"
            )
        );

        base.OnModelCreating(builder);
    }
}