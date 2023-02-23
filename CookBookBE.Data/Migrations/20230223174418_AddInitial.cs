using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBookBE.Data.Migrations
{
    public partial class AddInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbRecipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbRecipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbRecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbIngredient_DbRecipe_DbRecipeId",
                        column: x => x.DbRecipeId,
                        principalTable: "DbRecipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DbTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbRecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbTag_DbRecipe_DbRecipeId",
                        column: x => x.DbRecipeId,
                        principalTable: "DbRecipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbIngredient_DbRecipeId",
                table: "DbIngredient",
                column: "DbRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_DbTag_DbRecipeId",
                table: "DbTag",
                column: "DbRecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbIngredient");

            migrationBuilder.DropTable(
                name: "DbTag");

            migrationBuilder.DropTable(
                name: "DbRecipe");
        }
    }
}
