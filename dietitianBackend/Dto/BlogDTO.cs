namespace dietitianBackend.Dto
{
    public class BlogDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public List<int> CategoryIds { get; set; } // Kullanıcının seçeceği kategori ID'leri
    }
}
