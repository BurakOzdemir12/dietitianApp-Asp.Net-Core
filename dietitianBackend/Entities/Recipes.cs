namespace dietitianBackend.Entities
{
    public class Recipes
    {
        public int Id { get; set; }
        public string Name { get; set; }
         public string?  Img { get; set; }
        public string Category { get; set; }
        public string RecipeDetail{ get; set; }

        public int Kcal {  get; set; }
        public int Porsionsize {  get; set; }
        public int Cooktime {  get; set; }
        public int PreparationTime {  get; set; }
        public int TotalPorsiongram {  get; set; }


        public int RecipeCategoryId { get; set; }
        public virtual RecipeCategory RecipeCategory { get; set; }

        /*        public ICollection<RecipeRelationCategory> RecipeCategories { get; set; }
        */

    }
}
