namespace API.Interfaces;

public interface ICrudGenericRepository<T>
{
    IEnumerable<T> GetAll();
    
    T? GetById(int id);
    Task<T?> GetByIdAsync(int id);
    
    void Update(T item);
    
    void Create(T item);
    void CreateAsync(T item);
    
    void Delete(T item);
}