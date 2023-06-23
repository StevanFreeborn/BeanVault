namespace BeanVault.Services.ProductService.Infrastructure.Data.Mongo.Repositories;

public class MongoProductRepository : IProductRepository
{
  private readonly MongoDbContext _context;

  public MongoProductRepository(MongoDbContext context)
  {
    _context = context;
  }

  /// <summary>
  /// Adds the given product.
  /// </summary>
  /// <param name="coupon"></param>
  /// <returns>The newly created product.</returns>
  public async Task<Product> AddProductAsync(Product coupon)
  {
    await _context.Products.InsertOneAsync(coupon);
    return coupon;
  }

  /// <summary>
  /// Deletes a product with the given id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="ModelNotFoundException"></exception>
  public async Task DeleteProductByIdAsync(string id)
  {
    var deletedProduct = await _context.Products.FindOneAndDeleteAsync(p => p.Id == id);

    if (deletedProduct is null)
    {
      throw new ModelNotFoundException($"Unable to delete product with id: {id}");
    }
  }

  /// <summary>
  /// Get a product with the given id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns>A product</returns>
  /// <exception cref="ModelNotFoundException"></exception>
  public Task<Product> GetProductByIdAsync(string id)
  {
    var product = _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

    if (product == null)
    {
      throw new ModelNotFoundException($"Unable to find product with id: {id}");
    }

    return product;
  }

  /// <summary>
  /// Gets a list of products based on the given query.
  /// </summary>
  /// <param name="query"></param>
  /// <returns>A list of products</returns>
  public async Task<List<Product>> GetProductsAsync(ProductQuery query)
  {
    var products = _context.Products.AsQueryable();

    if (query.CategoryName != string.Empty)
    {
      products = products.Where(p => p.CategoryName.ToLower() == query.CategoryName.ToLower());
    }

    return await products.ToListAsync();
  }

  /// <summary>
  /// Updates the product with the given state.
  /// </summary>
  /// <param name="product"></param>
  /// <returns>The updated product.</returns>
  /// <exception cref="ModelNotFoundException"></exception>
  public async Task<Product> UpdateProductByIdAsync(Product product)
  {
    var updatedProduct = await _context.Products.FindOneAndReplaceAsync(
      p => p.Id == product.Id,
      product,
      new()
      {
        ReturnDocument = ReturnDocument.After,
      }
    );

    if (updatedProduct is null)
    {
      throw new ModelNotFoundException($"Unable to update product with id: {product.Id}");
    }

    return updatedProduct;
  }
}