using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateBank.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSuperAdminPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "PasswordHash",
                value: "$2a$11$KsnJwz4qaejE6xIrGd.RLOfnA6TYRQrYKYACRb0/nQA8iBoi1PT7y");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "PasswordHash",
                value: "$2a$11$or2Rg8NDeqq10APfQng1HO9zmM.LpqLc92QAR79ssJcgulD.HJgsu");
        }
    }
}
