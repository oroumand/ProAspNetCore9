using APISample.Core.Domain;
using APISample.Infra.Data.Ef.SQL;
using Microsoft.EntityFrameworkCore;

namespace APISample.Core.ApplicationService;

public class ProductsService
{
    private readonly ProductDbContext _dbContext;

    public ProductsService(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddProduct(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public List<Product> GetProdcuts() =>
        _dbContext.Products.ToList();

}
