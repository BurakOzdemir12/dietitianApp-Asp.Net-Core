namespace dietitianBackend.Entities
{
    public class Blogs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Img { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public ICollection<BlogCategory> BlogCategories { get; set; } 
            = new List<BlogCategory>(); 
      /*  One to Many Relation
       *  public int BlogCategoryId { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }
*/
    }
}
