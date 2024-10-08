using System.Text.Json.Serialization;

namespace dietitianBackend.Entities
{
    public class RecipeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Recipes> Recipes { get; set; } = new List<Recipes>();

        /*public ICollection<RecipeRelationCategory> RecipeCategories { get; set; }*/
    }
}
