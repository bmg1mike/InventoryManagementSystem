namespace Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly InventoryContext _context;

    public UnitOfWork(InventoryContext context)
    {
        _context = context;
    }

    public IProductRepository Product => new ProductRepository(_context);
    public IUserRepository User => new UserRepository(_context);


    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
}