namespace API.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    void CommitAsync();
    void Rollback();
    ICrudGenericRepository<T> GetCrudGenericRepository<T>() where T : class;
    ICustomRepository GetCustomRepository();
}