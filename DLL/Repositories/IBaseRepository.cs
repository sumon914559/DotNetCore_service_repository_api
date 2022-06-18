using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface IBaseRepository<T>
    {
        Task CreateAsync(T entity);


        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> QueryAll(Expression<Func<T, bool>> expression = null);
        Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);


        T Creates(T entity);

        void Update(T entity);
        void UpdateLatest(T entity);


        Task UpdateRange(List<T> entryList);
        Task CreateListAsync(List<T> entity);
        Task Delete(T entity);
        Task DeleteRange(List<T> entryList);
        //Task<IPagedList<T>> PaginateAsync(Expression<Func<T, bool>> expression, GeneralPaginationQuery paging = null);
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        protected int Save() => _context.SaveChanges();

        


        public virtual async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual async Task CreateListAsync(List<T> entity)
        {
            await _context.Set<T>().AddRangeAsync(entity);
        }


        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public T Creates(T entity)
        {
            var s = _context.Add(entity);
            Save();
            return entity;
        }


        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                return await _context.Set<T>().ToListAsync();
            }

            return await _context.Set<T>().Where(expression).ToListAsync();
        }


        public Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
        }


        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }


        public virtual async Task UpdateRange(List<T> entryList)
        {
            _context.Set<T>().UpdateRange(entryList);
        }

        public virtual void UpdateLatest(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual IQueryable<T> QueryAll(Expression<Func<T, bool>> expression)
        {
            return expression != null
                ? _context.Set<T>().AsQueryable().Where(expression)
                : _context.Set<T>().AsQueryable()
                    .AsNoTracking();
        }

        public virtual async Task Delete(T entity)
        {
          
            _context.Set<T>().Remove(entity);
        }

        public async Task DeleteRange(List<T> entryList)
        {
            
            _context.Set<T>().RemoveRange(entryList);
        }

        // public virtual async Task<IPagedList<T>> PaginateAsync(Expression<Func<T, bool>> expression,
        //     GeneralPaginationQuery paginationQuery = null)
        // {
        //     var query = QueryAll(expression);
        //
        //     query = query.OrderBy("Id DESC");
        //
        //     var gg = query.ToSql();
        //
        //
        //     return await query.ToPagedListAsync(paginationQuery.PageNumber,
        //         paginationQuery.Pagination ? paginationQuery.PageSize : 10000);
        // }
    }
}