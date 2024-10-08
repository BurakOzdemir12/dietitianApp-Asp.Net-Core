using System.Text.Json.Serialization;

namespace dietitianBackend.Entities
{
    public class FoodCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string? Img{ get; set; }

        [JsonIgnore]
        public ICollection<Food> Foods { get; set; }= new List<Food>();
    }
}
