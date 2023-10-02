using Microsoft.EntityFrameworkCore;

using API.Interfaces;
using API.Models;

namespace API.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly APIContext _context;
    private Dictionary<Type, object> _crudGenericRepositories;
    private CustomRepository _customRepository;

    public UnitOfWork(APIContext context)
    {
        _context = context;
        _crudGenericRepositories = new Dictionary<Type, object>();
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
    public async void CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Rollback()
    {
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
        }
    }
    
    public ICrudGenericRepository<T> GetCrudGenericRepository<T>() where T : class
    {
        if (_crudGenericRepositories.ContainsKey(typeof(T)))
        {
            return (ICrudGenericRepository<T>)_crudGenericRepositories[typeof(T)];
        }

        var repository = new CrudGenericRepository<T>(_context);
        _crudGenericRepositories.Add(typeof(T), repository);
        return repository;
    }

    public ICustomRepository GetCustomRepository()
    {
        if (_customRepository != null)
        {
            return _customRepository;
        }
        
        var repository = new CustomRepository(_context);
        _customRepository = repository;
        return repository;
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}