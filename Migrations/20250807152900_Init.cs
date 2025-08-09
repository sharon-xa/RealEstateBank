using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateBank.Migrations {
    /// <inheritdoc />
    public partial class Init : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDate", "Deleted", "Email", "FullName", "ModifiedDate", "Password", "PhoneNumber", "RefreshToken", "Role" },
                values: new object[] { new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"), new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), false, "SuperAdmin@SuperAdmin.com", "SuperAdmin", null, "$2a$11$or2Rg8NDeqq10APfQng1HO9zmM.LpqLc92QAR79ssJcgulD.HJgsu", "07816562345", null, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
