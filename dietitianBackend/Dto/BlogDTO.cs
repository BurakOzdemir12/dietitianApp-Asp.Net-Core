using Newtonsoft.Json;

namespace dietitianBackend.Dto
{

    public class BlogDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        // public List<int>? CategoryIds { get; set; } // Kullanıcının seçeceği kategori ID'leri
        // Backend tarafında gelen değeri parçalayarak `List<int>` haline getiriyoruz
        private string _categoryIds;
        public List<int>? CategoryIds
        {
            get
            {
                if (string.IsNullOrEmpty(_categoryIds)) return null;
                return JsonConvert.DeserializeObject<List<int>>(_categoryIds);
            }
            set
            {
                _categoryIds = value != null ? JsonConvert.SerializeObject(value) : null;
            }
        }
    }

}

