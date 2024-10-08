namespace dietitianBackend.Entities
{
    public class RecipeRelationCategory
    {
        public int RecipeId { get; set; }
        public Recipes Recipes { get; set; }

        public int CategoryId { get; set; }
        public RecipeCategory RecipeCategory { get; set; }
    }
}
