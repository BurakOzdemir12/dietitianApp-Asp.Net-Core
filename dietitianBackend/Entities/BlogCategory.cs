using System.Text.Json.Serialization;

namespace dietitianBackend.Entities
{
    public class BlogCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Blogs> Blogs { get; set; }=new List<Blogs>();
    }
}
