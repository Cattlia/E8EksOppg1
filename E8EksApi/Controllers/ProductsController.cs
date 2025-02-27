using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    //Get all http://localhost/api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
        var products = _productService.GetAllProducts();
        return Ok(products);
    }

    //Get product by id http://localhost/api/products/{id}
    [HttpGet("{id:int}")]
    public ActionResult<Product> GetProductById(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    //Get health status http://localhost/api/products/health
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok("API OK - Frisk som en fisk");
    }

}    