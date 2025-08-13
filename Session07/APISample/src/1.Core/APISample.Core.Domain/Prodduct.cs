using System.Globalization;
using System.Text.Json.Serialization;

namespace APISample.Core.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
    
        public Brand Brand { get; set; }

    }
}
