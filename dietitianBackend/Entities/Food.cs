using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace dietitianBackend.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Img { get; set; }
        public int Kcal {  get; set; }
        public int Protein {  get; set; }
        public int Fat {  get; set; }
        public int Carb {  get; set; }
        public int Fibr {  get; set; }
        public int Colest {  get; set; }
        public int Sodium {  get; set; }
        public int Potassium {  get; set; }
        public int Calsium {  get; set; }
        public int VitA {  get; set; }
        public int VitC {  get; set; }
        public int Iron {  get; set; }

        public int FoodcategoryId { get; set; }
        public virtual FoodCategories FoodCategory { get; set; }

        public virtual ICollection<Measurements> Measurements { get; set; } = new HashSet<Measurements>();

    }
}
