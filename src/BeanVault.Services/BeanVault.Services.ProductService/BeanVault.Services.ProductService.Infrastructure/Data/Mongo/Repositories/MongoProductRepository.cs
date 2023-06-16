namespace BeanVault.Services.ProductService.Infrastructure.Data.Mongo.Repositories;

public class MongoProductRepository : IProductRepository
{
  private readonly MongoDbContext _context;

  public MongoProductRepository(MongoDbContext context)
  {
    _context = context;
  }

  public async Task<List<Product>> GetProductsAsync(ProductQuery query)
  {
    var products = _context.Products.AsQueryable();

    if (query.CategoryName != string.Empty)
    {
      products = products.Where(p => p.CategoryName.ToLower() == query.CategoryName.ToLower());
    }

    return await products.ToListAsync();
  }
}