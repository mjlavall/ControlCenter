using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlCenter.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroceryLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Created = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroceryUnit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroceryItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Complete = table.Column<bool>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    GroceryListId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UnitId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroceryItem_GroceryLists_GroceryListId",
                        column: x => x.GroceryListId,
                        principalTable: "GroceryLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroceryItem_GroceryUnit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "GroceryUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroceryItem_GroceryListId",
                table: "GroceryItem",
                column: "GroceryListId");

            migrationBuilder.CreateIndex(
                name: "IX_GroceryItem_UnitId",
                table: "GroceryItem",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroceryItem");

            migrationBuilder.DropTable(
                name: "GroceryLists");

            migrationBuilder.DropTable(
                name: "GroceryUnit");
        }
    }
}
