using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
    {
        public static IQueryable<TEntity> Query(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec) {
            var query = inputQuery;

            if(spec.Criteria != null) {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (a, include) => a.Include(include));

            return query;
        }
    }
}