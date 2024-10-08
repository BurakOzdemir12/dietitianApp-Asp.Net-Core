using dietitianBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace dietitianBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<RecipeCategory> RecipeCategory { get; set; }
        public DbSet<Recipes> Recipes { get; set; }

        public DbSet<Food> Foods { get; set; }                   
        public DbSet<FoodCategories> FoodCategories { get; set; }
       
        public DbSet<Measurements> Measurements { get; set; }

/*        public DbSet<RecipeRelationCategory> RecipeRelationCategory { get; set; }
*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipes>()
                .HasOne(r => r.RecipeCategory)
                .WithMany(r => r.Recipes)
                .HasForeignKey(r => r.RecipeCategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Food>()
                .HasOne(r => r.FoodCategory)
                .WithMany(r => r.Foods)
                .HasForeignKey(r => r.FoodcategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Measurements>()
                .HasOne(r => r.Food)
                .WithMany(r => r.Measurements)
                .HasForeignKey(r => r.FoodId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}