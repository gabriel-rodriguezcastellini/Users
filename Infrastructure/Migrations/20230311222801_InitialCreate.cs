using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace Infrastructure.Migrations;

[ExcludeFromCodeCoverage]
/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "The name of the user", collation: "SQL_Latin1_General_CP1_CI_AS"),
                Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The email of the user"),
                Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "The address of the user"),
                Phone = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "The phone of the user"),
                Type = table.Column<int>(type: "int", nullable: false, comment: "The user type"),
                Money = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, comment: "The money of the user")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_Name_Address",
            table: "Users",
            columns: new[] { "Name", "Address" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_Phone",
            table: "Users",
            column: "Phone",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users");
    }
}
