using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddSubToFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Features",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Features_SubCategoryId",
                table: "Features",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_SubCategories_SubCategoryId",
                table: "Features",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_SubCategories_SubCategoryId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_SubCategoryId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Features");
        }
    }
}
