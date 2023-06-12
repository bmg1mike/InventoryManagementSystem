namespace Data;

public class UserRepository : RepositoryBase<AppUser>, IUserRepository
{
    private readonly InventoryContext _context;

    public UserRepository(InventoryContext context) : base(context)
    {
        _context = context;
    }
}