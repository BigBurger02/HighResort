using Microsoft.EntityFrameworkCore;

using API.Interfaces;

namespace API.Data;

public class CrudGenericRepository<T> : ICrudGenericRepository<T> where T : class
{
    private readonly APIContext _context;
    private readonly DbSet<T> _dbSet;

    public CrudGenericRepository(APIContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.AsNoTracking().AsEnumerable();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Update(T item)
    {
        _context.Entry(item).State = EntityState.Modified;
    }

    public void Create(T item)
    {
        _dbSet.Add(item);
    }
    public async void CreateAsync(T item)
    {
        await _dbSet.AddAsync(item);
    }

    public void Delete(T item)
    {
        _dbSet.Remove(item);
    }
}