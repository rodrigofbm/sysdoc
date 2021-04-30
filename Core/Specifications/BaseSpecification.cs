
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity: BaseEntity
  {
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
      Criteria = criteria;
    }

    public Expression<Func<TEntity, bool>> Criteria { get; }
    public IList<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
  
    protected void AddIncludes(Expression<Func<TEntity, object>> includeExpression)
    {
      Includes.Add(includeExpression);
    }
  }
}