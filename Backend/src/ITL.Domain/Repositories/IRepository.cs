using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITL.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(int id, IEnumerable<string> entitiesToInclude = null);
    Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates = null, IEnumerable<string> entitiesToInclude = null);
    Task<IEnumerable<TEntity>> GetAllAsync(int skip, int limit, IEnumerable<Expression<Func<TEntity, bool>>> predicates = null, IEnumerable<string> entitiesToInclude = null);
    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<int> Count(IEnumerable<Expression<Func<TEntity, bool>>> predicates = null);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
}
