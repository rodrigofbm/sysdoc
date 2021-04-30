using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Data.Repositories
{
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
  {
    private readonly ApplicationDbContext _context;
    public Repository(ApplicationDbContext context)
    {
      _context = context;
    }

    public void Add(TEntity entity)
    {
      _context.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
      _context.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
      _context.Set<TEntity>().Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
      return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification)
    {
      return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
      return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> GetEntityWithSpec(ISpecification<TEntity> specification)
    {
      return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification) {
        return SpecificationEvaluator<TEntity>.Query(_context.Set<TEntity>().AsQueryable(), specification);
    }
  }
}