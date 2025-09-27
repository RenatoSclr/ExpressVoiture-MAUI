using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoiture.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateImagePathSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 1,
                column: "ImagePath",
                value: "images/vehicles/d950d976-c3db-4e9a-a465-6422ef06f949.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 2,
                column: "ImagePath",
                value: "images/vehicles/ba52fc34-a4eb-446f-aa19-078a26829f29.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 3,
                column: "ImagePath",
                value: "images/vehicles/41597198-aaea-461b-af70-c9b4505e9298.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 4,
                column: "ImagePath",
                value: "images/vehicles/fa6b80a2-f4f9-44da-bf16-6e8bd5e2d1a0.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 5,
                column: "ImagePath",
                value: "images/vehicles/633fd8d9-4550-4269-a508-5a08e1fa564b.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 6,
                column: "ImagePath",
                value: "images/vehicles/f0c751ce-6cf0-4fcf-9ac8-1f125269dec3.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 7,
                column: "ImagePath",
                value: "images/vehicles/c93a06dd-2e25-4776-91f4-d999779cad87.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 1,
                column: "ImagePath",
                value: "images\\vehicles\\d950d976-c3db-4e9a-a465-6422ef06f949.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 2,
                column: "ImagePath",
                value: "images\\vehicles\\ba52fc34-a4eb-446f-aa19-078a26829f29.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 3,
                column: "ImagePath",
                value: "images\\vehicles\\41597198-aaea-461b-af70-c9b4505e9298.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 4,
                column: "ImagePath",
                value: "images\\vehicles\\fa6b80a2-f4f9-44da-bf16-6e8bd5e2d1a0.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 5,
                column: "ImagePath",
                value: "images\\vehicles\\633fd8d9-4550-4269-a508-5a08e1fa564b.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 6,
                column: "ImagePath",
                value: "images\\vehicles\\f0c751ce-6cf0-4fcf-9ac8-1f125269dec3.jpg");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "VoitureId",
                keyValue: 7,
                column: "ImagePath",
                value: "images\\vehicles\\c93a06dd-2e25-4776-91f4-d999779cad87.jpg");
        }
    }
}
