using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetEntityWithSpec(ISpecification<TEntity> specification);
        Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}