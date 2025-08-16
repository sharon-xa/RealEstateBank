using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateBank.Migrations
{
    /// <inheritdoc />
    public partial class AddBankFirstRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bank",
                columns: new[] { "Id", "AboutUs", "BankBusiness", "BankEstablishment", "BankServices", "VideoDescription", "VideoTitle", "VideoUrl" },
                values: new object[] { 1, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bank",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
