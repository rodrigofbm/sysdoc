using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class UnityOfWork : IUnityOfWork
  {
    private readonly DbContext _context;
    private Hashtable _repositories;

    public UnityOfWork(DbContext context)
    {
        _context = context;
    }

    public async Task<int> Complete()
    {
      return await _context.SaveChangesAsync();
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
      if(_repositories == null) _repositories = new Hashtable();
      var type = typeof(TEntity);

      if(!_repositories.Contains(type.Name)) {
          var repository = typeof(Repository<>);
          var instance = Activator.CreateInstance(repository.MakeGenericType(type), _context);

          _repositories.Add(type.Name, instance);
      }

      return (IRepository<TEntity>)_repositories[type.Name];
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}