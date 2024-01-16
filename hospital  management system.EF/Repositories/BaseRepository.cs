using AutoMapper;
using AutoMapper.QueryableExtensions;
using hospital__management_system.Core.Constants;
using hospital__management_system.Core.Interfaces;
using hospital__management_system.Core.Models;
using hospital__management_system.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BaseRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public T Find(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.FirstOrDefault(criteria);
        }
        
        public TDto Find<TDto>(Expression<Func<T, bool>> criteria)
        {
            return _context
                .Set<T>()
                .Where(criteria)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public async Task<T> FindAsync
            (Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<TDto> FindAsync<TDto>
            (Expression<Func<T, bool>> criteria)
        {
            return await _context
                .Set<T>()
                .Where(criteria)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }



        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }
        
        public IEnumerable<TDto> FindAll<TDto>(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>()
                .Where(criteria)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public IEnumerable<T> FindAllPaginated
            (Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }

        public async Task<object> FindWithSelectAsync(Expression<Func<T, bool>> criteria,
           Expression<Func<T, object>> selects = null, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            if (selects != null)
                return await query.Where(criteria).Select(selects).FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync(criteria);
        }

        public IEnumerable<TDto> FindAllPaginated<TDto>
            (Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync
            (Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<TDto>> FindAllAsync<TDto>(Expression<Func<T, bool>> criteria)
        {
            return await _context
                .Set<T>()
                .Where(criteria)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllPaginatedAsync
            (Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TDto>> FindAllPaginatedAsync<TDto>
            (Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query
                    .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
            
        }
    }
}
