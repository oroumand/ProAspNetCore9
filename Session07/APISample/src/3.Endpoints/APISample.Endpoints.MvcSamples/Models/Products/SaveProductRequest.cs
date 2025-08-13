using System.ComponentModel.DataAnnotations;

namespace APISample.Endpoints.MvcSamples.Models.Products;

public class SaveProductRequest
{
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string Title { get; set; }
    [Required]
    [MaxLength(500)]
    [MinLength(20)]
    public string Description { get; set; }
    public int BrandId { get; set; }
}
