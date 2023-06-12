namespace Services;

public interface IProductService
{
    Task<string> AddProduct(AddProductDto request);
    Task<PaginationResponse<Product>> GetProducts();
    Task<Product> GetProduct(string productId);
}

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> AddProduct(AddProductDto request)
    {
        try
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
                OwnerId = "1505e779-6434-4765-bbb4-5a8428107ee8"
            };
            await _unitOfWork.Product.AddAsync(product);

            if (!await _unitOfWork.Complete())
            {
                _logger.LogInformation("Could't save product to the database");
                return string.Empty;
            }

            return product.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return string.Empty;
        }
    }

    public async Task<PaginationResponse<Product>> GetProducts()
    {
        var productsFromDb = await _unitOfWork.Product.GetAllAsync();
        // var products = new PaginationResponse<GetProductssDto>
        // {
        //     CurrentPage = productsFromDb.CurrentPage,
        //     Data = _mapper.Map<PaginatedList<GetProductssDto>>(productsFromDb.Data),
        //     PageSize = productsFromDb.PageSize,
        //     TotalCount = productsFromDb.TotalCount,
        //     TotalPages = productsFromDb.TotalPages
        // };
        //var products = _mapper.Map<PaginationResponse<GetProductssDto>>(productsFromDb);
        return productsFromDb;
    }

    public async Task<Product> GetProduct(string productId)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(productId);
        return product;
    }
}