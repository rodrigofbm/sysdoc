using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnityOfWork: IDisposable
    {
        Task<int> Complete();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity: BaseEntity;
    }
}