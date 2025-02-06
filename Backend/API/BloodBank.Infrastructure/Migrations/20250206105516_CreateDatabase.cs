using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    RhFactor = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RhFactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityML = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HealthPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityMl = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_HealthPosts_HealthPostId",
                        column: x => x.HealthPostId,
                        principalTable: "HealthPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "HealthPosts",
                columns: new[] { "Id", "Address", "City", "Name" },
                values: new object[,]
                {
                    { new Guid("0a390b24-257c-484f-b930-1bc1c8e7ab8c"), "Rua Almirante Tamandaré, 150", "Santana de Parnaíba", "Hospital Santa Ana" },
                    { new Guid("0bfaf448-bd38-47d2-92c2-4668f99cd7f0"), "Rua Benedito de Lima, 250", "Itapevi", "Hospital Geral de Itapevi" },
                    { new Guid("18028a3e-f4ad-45be-b586-9c63c4627e12"), "Av. Deputado Emílio Carlos, 500", "Carapicuíba", "UPA Carapicuíba" },
                    { new Guid("3804fb9d-f363-4b9e-86bf-9ae37e1a1142"), "Rua Pedro Fioretti, 1123", "Osasco", "UPA Conceição" },
                    { new Guid("40457671-1052-4922-bda7-dc721e4833bd"), "Rua Amazonas, 45", "Carapicuíba", "UBS Inac" },
                    { new Guid("416921ca-7207-444c-a198-6ff2b0bf1bdf"), "Av. Lions Internacional, 1250", "Cotia", "UBS Portão" },
                    { new Guid("47201fd2-7061-49a4-b183-a9fd312581bf"), "Rua São Roque, 1200", "Jandira", "UPA Jandira" },
                    { new Guid("4a226750-903f-45e3-a1ec-5b76077ad3df"), "Av. Sport Club Corinthians Paulista, 345", "Osasco", "UBS Santo Antônio" },
                    { new Guid("4aeed005-1117-490b-bf22-e5a24fd95e91"), "Rua Esmeralda, 85", "Cotia", "Hospital Regional de Cotia" },
                    { new Guid("4e432145-cecb-4417-a3a3-f730d444d1b4"), "Rua São João, 120", "Carapicuíba", "UBS Vila Dirce" },
                    { new Guid("560da268-36fd-4708-9d58-6b38bb728560"), "Av. Tenente Marques, 980", "Santana de Parnaíba", "UBS Fazendinha" },
                    { new Guid("756d796f-9a24-4f93-a3ae-1da248bc4b8f"), "Av. Presidente Costa e Silva, 250", "Jandira", "UBS Jardim Gabriela" },
                    { new Guid("948e1a29-0a9b-4394-b748-6296cc7a1513"), "Av. Vinte e Seis de Março, 800", "Barueri", "UPA Barueri" },
                    { new Guid("a4e7b36a-c336-4dd2-b415-11c4a52e1f44"), "Alameda Rio Negro, 999", "Santana de Parnaíba", "UPA Alphaville" },
                    { new Guid("bae4338d-aa25-48e6-9885-68c595a8fb12"), "Estr. Morro Grande, 745", "Cotia", "UPA Atalaia" },
                    { new Guid("bc827713-406a-4325-a1e2-353911364a4f"), "Av. Rubens Caramez, 987", "Itapevi", "UBS Jardim Briquet" },
                    { new Guid("c06928e9-1545-44d3-827f-611cb493b037"), "Rua Carmem, 112", "Jandira", "Hospital Municipal de Jandira" },
                    { new Guid("cc7f40bf-0ba8-4a91-aeae-c772fbb965f1"), "Rua Ângela Mirella, 354", "Barueri", "Hospital Municipal de Barueri" },
                    { new Guid("d5259026-0931-4fcb-bc98-63f0683ecbe2"), "Rua Itajubá, 220", "Barueri", "UBS Jardim Belval" },
                    { new Guid("eda86541-3ecb-4bf6-8ec0-771cf481699f"), "Rua Cesário Verde, 65", "Itapevi", "UPA Itapevi" },
                    { new Guid("f39a504a-0b7e-44fc-908b-506d97d878c9"), "Rua da Estação, 200", "Osasco", "Hospital Antonio Giglio" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorId",
                table: "Donations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_HealthPostId",
                table: "Donations",
                column: "HealthPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_Email",
                table: "Donors",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "HealthPosts");
        }
    }
}
