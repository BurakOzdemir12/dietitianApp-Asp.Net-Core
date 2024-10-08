using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dietitianBackend.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeCategorie_RecipeCategoryId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeCategorie",
                table: "RecipeCategorie");

            migrationBuilder.RenameTable(
                name: "RecipeCategorie",
                newName: "RecipeCategory");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeCategoryId",
                table: "Recipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeCategory_RecipeCategoryId",
                table: "Recipes",
                column: "RecipeCategoryId",
                principalTable: "RecipeCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeCategory_RecipeCategoryId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory");

            migrationBuilder.RenameTable(
                name: "RecipeCategory",
                newName: "RecipeCategorie");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeCategoryId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeCategorie",
                table: "RecipeCategorie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeCategorie_RecipeCategoryId",
                table: "Recipes",
                column: "RecipeCategoryId",
                principalTable: "RecipeCategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
