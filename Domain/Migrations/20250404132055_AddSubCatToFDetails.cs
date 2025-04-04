using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddSubCatToFDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "FeatureDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FeatureDetails_SubCategoryId",
                table: "FeatureDetails",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureDetails_SubCategories_SubCategoryId",
                table: "FeatureDetails",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureDetails_SubCategories_SubCategoryId",
                table: "FeatureDetails");

            migrationBuilder.DropIndex(
                name: "IX_FeatureDetails_SubCategoryId",
                table: "FeatureDetails");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "FeatureDetails");
        }
    }
}
