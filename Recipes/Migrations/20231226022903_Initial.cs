using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recipes.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserLevel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DifficultyLevel = table.Column<decimal>(type: "TEXT", nullable: false),
                    Section = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "FullName", "Phone", "UserLevel" },
                values: new object[,]
                {
                    { new Guid("4b6a1054-4fe8-4ce1-b6ac-6180b1da7095"), new DateTime(2000, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "nefedova.v@gmail.com", "Nefedova Vasilisa Fedorovna", "+375(29)783-56-05", 0 },
                    { new Guid("9257793e-8cb7-11ee-a4a2-80e82c270b17"), new DateTime(2002, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "smirnova123456@mail.ru", "Smirnova Alena Rostislavovna", "+375(33)853-86-98", 0 }
                });

            migrationBuilder.InsertData(
                table: "Recipies",
                columns: new[] { "Id", "CreatedOn", "Description", "DifficultyLevel", "Name", "Section", "UserId" },
                values: new object[] { new Guid("03d5a75a-46ce-4c7f-9a12-f762f2331dc5"), new DateTime(2023, 12, 26, 5, 29, 2, 122, DateTimeKind.Local).AddTicks(5334), "You'll find the full, step-by-step recipe below — but here's a brief overview of what you can expect when you make baked potato soup at home:  Cook the bacon.  Melt the butter, then whisk in the flour and milk.  Add the potatoes and onions.", 3.3m, "Baked Potato Soup", 3, new Guid("4b6a1054-4fe8-4ce1-b6ac-6180b1da7095") });

            migrationBuilder.CreateIndex(
                name: "IX_Recipies_UserId",
                table: "Recipies",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
