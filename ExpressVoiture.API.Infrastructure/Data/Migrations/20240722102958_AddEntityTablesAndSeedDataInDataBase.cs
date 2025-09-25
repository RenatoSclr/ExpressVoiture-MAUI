using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpressVoiture.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityTablesAndSeedDataInDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voitures",
                columns: table => new
                {
                    VoitureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Annee = table.Column<int>(type: "int", nullable: false),
                    Marque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modele = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrixAchat = table.Column<double>(type: "float", nullable: false),
                    CodeVIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voitures", x => x.VoitureId);
                });

            migrationBuilder.CreateTable(
                name: "Reparation",
                columns: table => new
                {
                    ReparationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cout = table.Column<double>(type: "float", nullable: false),
                    VoitureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparation", x => x.ReparationId);
                    table.ForeignKey(
                        name: "FK_Reparation_Voitures_VoitureId",
                        column: x => x.VoitureId,
                        principalTable: "Voitures",
                        principalColumn: "VoitureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vente",
                columns: table => new
                {
                    VenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDisponibiliteVente = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrixVente = table.Column<double>(type: "float", nullable: false),
                    DateVente = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoitureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vente", x => x.VenteId);
                    table.ForeignKey(
                        name: "FK_Vente_Voitures_VoitureId",
                        column: x => x.VoitureId,
                        principalTable: "Voitures",
                        principalColumn: "VoitureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Voitures",
                columns: new[] { "VoitureId", "Annee", "CodeVIN", "DateAchat", "Finition", "ImagePath", "Marque", "Modele", "PrixAchat" },
                values: new object[,]
                {
                    { 1, 2019, null, new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "LE", "images\\vehicles\\d950d976-c3db-4e9a-a465-6422ef06f949.jpg", "Mazda", "Miata", 1800.0 },
                    { 2, 2007, null, new DateTime(2022, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sport", "images\\vehicles\\ba52fc34-a4eb-446f-aa19-078a26829f29.jpg", "Jeep", "Liberty", 4500.0 },
                    { 3, 2007, null, new DateTime(2022, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "TCe", "images\\vehicles\\41597198-aaea-461b-af70-c9b4505e9298.jpg", "Renault", "Scénic", 1800.0 },
                    { 4, 2017, null, new DateTime(2022, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "XLT", "images\\vehicles\\fa6b80a2-f4f9-44da-bf16-6e8bd5e2d1a0.jpg", "Ford", "Explorer", 24350.0 },
                    { 5, 2008, null, new DateTime(2022, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "LX", "images\\vehicles\\633fd8d9-4550-4269-a508-5a08e1fa564b.jpg", "Honda", "Civic", 4000.0 },
                    { 6, 2016, null, new DateTime(2022, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "S", "images\\vehicles\\f0c751ce-6cf0-4fcf-9ac8-1f125269dec3.jpg", "Volkswagen", "GTI", 15250.0 },
                    { 7, 2013, null, new DateTime(2022, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "SEL", "images\\vehicles\\c93a06dd-2e25-4776-91f4-d999779cad87.jpg", "Ford", "Edge", 10990.0 }
                });

            migrationBuilder.InsertData(
                table: "Reparation",
                columns: new[] { "ReparationId", "Cout", "Description", "VoitureId" },
                values: new object[,]
                {
                    { 1, 7600.0, "Restauration complète", 1 },
                    { 2, 350.0, "Roulements des roues avant", 2 },
                    { 3, 690.0, "Radiateur, freins", 3 },
                    { 4, 1100.0, "Pneus, freins", 4 },
                    { 5, 475.0, "Climatisation, freins", 5 },
                    { 6, 440.0, "Pneus", 6 },
                    { 7, 950.0, "Pneus, freins, climatisation", 7 }
                });

            migrationBuilder.InsertData(
                table: "Vente",
                columns: new[] { "VenteId", "DateDisponibiliteVente", "DateVente", "PrixVente", "VoitureId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 7600.0, 1 },
                    { 2, new DateTime(2022, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 5350.0, 2 },
                    { 3, new DateTime(2022, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2990.0, 3 },
                    { 4, new DateTime(2022, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 25950.0, 4 },
                    { 5, new DateTime(2022, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4975.0, 5 },
                    { 6, new DateTime(2022, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 16190.0, 6 },
                    { 7, new DateTime(2022, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12440.0, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reparation_VoitureId",
                table: "Reparation",
                column: "VoitureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vente_VoitureId",
                table: "Vente",
                column: "VoitureId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reparation");

            migrationBuilder.DropTable(
                name: "Vente");

            migrationBuilder.DropTable(
                name: "Voitures");
        }
    }
}
