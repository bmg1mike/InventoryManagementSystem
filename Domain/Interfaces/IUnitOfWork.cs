namespace Domain;

public interface IUnitOfWork
{
    IProductRepository Product { get; }
    IUserRepository User { get; }
    Task<bool> Complete();
    bool HasChanges();
}