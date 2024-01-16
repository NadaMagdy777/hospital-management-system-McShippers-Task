using hospital__management_system.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Find(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null);
        TDto Find<TDto>(Expression<Func<T, bool>> criteria);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null);

        IEnumerable<TDto> FindAll<TDto>(Expression<Func<T, bool>> criteria);
        
        IEnumerable<T> FindAllPaginated
            (Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        
        IEnumerable<TDto> FindAllPaginated<TDto>
            (Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<T> FindAsync(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null);

        Task<TDto> FindAsync<TDto>(Expression<Func<T, bool>> criteria);
        
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null);
        
        Task<IEnumerable<TDto>> FindAllAsync<TDto>(Expression<Func<T, bool>> criteria);

        Task<IEnumerable<T>> FindAllPaginatedAsync
            (Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
       
        Task<IEnumerable<TDto>> FindAllPaginatedAsync<TDto>
            (Expression<Func<T, bool>> criteria, int? take, int? skip, 
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        
        Task<T> Add(T entity);
        
        T Update(T entity);
        
        void Delete(T entity);

        Task<object> FindWithSelectAsync(Expression<Func<T, bool>> criteria,
           Expression<Func<T, object>> selects = null, List<Expression<Func<T, object>>> includes = null);
    }
}
