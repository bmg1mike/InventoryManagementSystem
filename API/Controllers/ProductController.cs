namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct(AddProductDto request)
    {
        return Ok(await _productService.AddProduct(request));
    }

    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await _productService.GetProducts());
    }

    [HttpGet("GetProductById/{productId}")]
    public async Task<IActionResult> GetProductById(string productId)
    {
        return Ok(await _productService.GetProduct(productId));
    }
}