using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dietitianBackend.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeRelationCategory");

            migrationBuilder.AddColumn<int>(
                name: "RecipeCategoryId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeCategoryId",
                table: "Recipes",
                column: "RecipeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeCategorie_RecipeCategoryId",
                table: "Recipes",
                column: "RecipeCategoryId",
                principalTable: "RecipeCategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeCategorie_RecipeCategoryId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_RecipeCategoryId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeCategoryId",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "RecipeRelationCategory",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRelationCategory", x => new { x.RecipeId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_RecipeRelationCategory_RecipeCategorie_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "RecipeCategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeRelationCategory_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRelationCategory_CategoryId",
                table: "RecipeRelationCategory",
                column: "CategoryId");
        }
    }
}
