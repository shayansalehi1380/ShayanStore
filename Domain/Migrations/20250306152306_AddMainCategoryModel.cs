using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddMainCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MainCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MainCategories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MainCategories_MainCategoryId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "MainCategories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MainCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");
        }
    }
}
