namespace APISample.Core.Domain
{
    public class Brand
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Product> Products { get; set; }
    }
}
