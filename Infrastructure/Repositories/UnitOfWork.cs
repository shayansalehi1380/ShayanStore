using System.Collections;
using Application.Interface;
using Domain.Common;
using Domain.DBContext;

namespace Infrastructure.Repositories;

public class UnitOfWork(ShayanStoreDBContext _db):IUnitOfWork
{
    private Hashtable? _repositories;
    public IGenericRepository<T> GenericRepository<T>() where T : BaseEntity
    {
        if (_repositories is null)
            _repositories = new Hashtable();
        
        var type = typeof(T).Name;
        
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _db);

            _repositories.Add(type, repositoryInstance);
        }
        return (IGenericRepository<T>)_repositories[type];
    }
}