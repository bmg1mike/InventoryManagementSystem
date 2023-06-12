namespace Data;

public class ProductRepository : RepositoryBase<Product>,IProductRepository
{
    private readonly InventoryContext db;
    public ProductRepository(InventoryContext db) : base(db)
    {
        this.db = db;
    }
}