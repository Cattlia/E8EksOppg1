

 

public class ProductService
{
    private readonly ProductContext _productContext;

    public ProductService(ProductContext productContext)
    {
        _productContext = productContext;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        var products = _productContext.products
            .Where(p => p.Stock > 0)
            .ToList();
        return products;
    }
    public Product? GetProductById(int id)
    {
        return _productContext.products
          .FirstOrDefault(p => p.Id == id && p.Stock > 0);
    }
}

