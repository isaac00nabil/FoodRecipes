using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipes_Core.Migrations
{
    /// <inheritdoc />
    public partial class FoodRecipesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Admins");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 866, DateTimeKind.Local).AddTicks(5857),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 136, DateTimeKind.Local).AddTicks(8042));

            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "FoodSections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 863, DateTimeKind.Local).AddTicks(2507),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 135, DateTimeKind.Local).AddTicks(1430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Donations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 863, DateTimeKind.Local).AddTicks(42),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 134, DateTimeKind.Local).AddTicks(8992));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 862, DateTimeKind.Local).AddTicks(5751),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 134, DateTimeKind.Local).AddTicks(4432));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Clients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 862, DateTimeKind.Local).AddTicks(2364),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 134, DateTimeKind.Local).AddTicks(1197));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Admins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 861, DateTimeKind.Local).AddTicks(8135),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 133, DateTimeKind.Local).AddTicks(7193));

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginId);
                    table.ForeignKey(
                        name: "FK_Logins_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId");
                    table.ForeignKey(
                        name: "FK_Logins_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_LoginId",
                table: "Reviews",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_AdminId",
                table: "Logins",
                column: "AdminId",
                unique: true,
                filter: "[AdminId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_ClientId",
                table: "Logins",
                column: "ClientId",
                unique: true,
                filter: "[ClientId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Logins_LoginId",
                table: "Reviews",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "LoginId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Logins_LoginId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_LoginId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Reviews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 136, DateTimeKind.Local).AddTicks(8042),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 866, DateTimeKind.Local).AddTicks(5857));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "FoodSections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 135, DateTimeKind.Local).AddTicks(1430),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 863, DateTimeKind.Local).AddTicks(2507));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Donations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 134, DateTimeKind.Local).AddTicks(8992),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 863, DateTimeKind.Local).AddTicks(42));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 134, DateTimeKind.Local).AddTicks(4432),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 862, DateTimeKind.Local).AddTicks(5751));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Clients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 134, DateTimeKind.Local).AddTicks(1197),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 862, DateTimeKind.Local).AddTicks(2364));

            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Admins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 133, DateTimeKind.Local).AddTicks(7193),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 5, 16, 39, 20, 861, DateTimeKind.Local).AddTicks(8135));

            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
