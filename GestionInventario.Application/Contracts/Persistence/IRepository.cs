using GestionInventario.Domain.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Contracts.Persistence
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    string includeString = null,
                    bool disableTracking = true
            );
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    List<Expression<Func<TEntity, object>>> includes = null,
                    bool disableTracking = true
            );
        Task<TEntity> GetFirstAsync(
           Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includes = null,
           bool disableTracking = true,
           CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes = null);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<int> DeleteAsync(TEntity entity);
        void AddEntity(TEntity entity);

        void UpdateEntity(TEntity entity);

        void DeleteEntity(TEntity entity);

        IQueryable<TEntity> GetQueryable(List<Expression<Func<TEntity, object>>> includes = null);

        Task<PagedResult<TEntity>> QueryPagedAsync(IQueryable<TEntity> query,
            int pageNumber,
            int pageSize,
            Expression<Func<TEntity, object>> orderByExpression,
            bool isDescending = false);
        Task<List<TEntity>> QueryToListAsync(IQueryable<TEntity> query);
        Task<int> ObtenerCodigoAsync(Expression<Func<TEntity, int?>> selector);


    }
}
