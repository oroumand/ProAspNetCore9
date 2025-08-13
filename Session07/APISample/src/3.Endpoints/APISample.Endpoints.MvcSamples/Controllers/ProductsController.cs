using APISample.Core.ApplicationService;
using APISample.Core.Domain;
using APISample.Endpoints.MvcSamples.Models.Products;
using APISample.Infra.Data.Ef.SQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APISample.Endpoints.MvcSamples.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductsService _service;

    public ProductsController(ProductsService service)
    {
        _service = service;
    }
    [HttpGet("Products")]
    [Produces("application/json", "applicatoin/xml")]

    public IActionResult Get()
    {

        return Ok(_service.GetProdcuts());
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> Save([FromBody] SaveProductRequest product)
    {
        _service.AddProduct(new Product
        {
            BrandId = product.BrandId,
            Title = product.Title,
            Description = product.Description,
        });
        return Created();
    }
    [HttpPost]
    [Consumes("application/xml")]
    public async Task<IActionResult> Save1([FromBody] SaveProductRequest product)
    {
        _service.AddProduct(new Product
        {
            BrandId = product.BrandId,
            Title = product.Title,
            Description = product.Description,
        });
        return Created();
    }
}
