using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipes_Core.Migrations
{
    /// <inheritdoc />
    public partial class sd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Admin"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 133, DateTimeKind.Local).AddTicks(7193)),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.CheckConstraint("CH_DisplayName_Length", "LEN(DisplayName)>=3 AND LEN(DisplayName)<=50");
                    table.CheckConstraint("CH_Password_Length", "LEN([Password])>=8 AND LEN([Password])<=20");
                    table.CheckConstraint("CH_Username_Length", "LEN(Username)>=3 AND LEN(Username)<=254");
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Client"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 134, DateTimeKind.Local).AddTicks(1197)),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.CheckConstraint("CH_Email_Length", "LEN(Email)>=13 AND LEN(Email)<=254");
                    table.CheckConstraint("CH_Email_Like", "Email Like '%@gmail.com%' OR Email Like  '%@outlook.com%' OR Email Like '%@yahoo.com%'");
                    table.CheckConstraint("CH_FirstName_Length", "LEN(FirstName)>=3 AND LEN(FirstName)<=20");
                    table.CheckConstraint("CH_LastName_Length", "LEN(LastName)>=3 AND LEN(LastName)<=20");
                    table.CheckConstraint("CH_Password_Length1", "LEN([Password])>=8 AND LEN([Password])<=20");
                    table.CheckConstraint("CH_Username_Length1", "LEN(Username)>=3 AND LEN(Username)<=50");
                });

            migrationBuilder.CreateTable(
                name: "FoodSections",
                columns: table => new
                {
                    FoodSectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 135, DateTimeKind.Local).AddTicks(1430)),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodSections", x => x.FoodSectionId);
                    table.CheckConstraint("CH_SectionName_NameOfSection", "LEN(SectionName)>=10 AND LEN(SectionName)<=16");
                    table.ForeignKey(
                        name: "FK_FoodSections_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    DonationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardType = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 134, DateTimeKind.Local).AddTicks(8992)),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.DonationId);
                    table.CheckConstraint("CH_CardType", "LEN(CardType)>=100 AND LEN(CardType)<=104 ");
                    table.CheckConstraint("CH_PaymentMethod_Type", "PaymentMethod>=0 AND PaymentMethod<=1 ");
                    table.CheckConstraint("CH_Point_Count", "Point>=1");
                    table.CheckConstraint("CH_Price_Value", "Price>0");
                    table.ForeignKey(
                        name: "FK_Donations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 136, DateTimeKind.Local).AddTicks(8042)),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.CheckConstraint("CH_Rating_Range", "Rating>=0 AND Rating<=10");
                    table.ForeignKey(
                        name: "FK_Reviews_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    DishId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DishName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Steps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodSectionId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 5, 15, 3, 44, 134, DateTimeKind.Local).AddTicks(4432)),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.DishId);
                    table.CheckConstraint("CH_Description_Length", "LEN(Description)>=50 AND LEN(Description)<=255");
                    table.CheckConstraint("CH_DishName_Length", "LEN(DishName)>=2 AND LEN(DishName)<=30");
                    table.CheckConstraint("CH_Steps_Length", "LEN(Steps)>=30 AND LEN(Steps)<=255");
                    table.ForeignKey(
                        name: "FK_Dishes_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dishes_FoodSections_FoodSectionId",
                        column: x => x.FoodSectionId,
                        principalTable: "FoodSections",
                        principalColumn: "FoodSectionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Username",
                table: "Admins",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Username",
                table: "Clients",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_AdminId",
                table: "Dishes",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_DishName",
                table: "Dishes",
                column: "DishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_FoodSectionId",
                table: "Dishes",
                column: "FoodSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CardType",
                table: "Donations",
                column: "CardType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_ClientId",
                table: "Donations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodSections_AdminId",
                table: "FoodSections",
                column: "AdminId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodSections_SectionName",
                table: "FoodSections",
                column: "SectionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews",
                column: "ClientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "FoodSections");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
