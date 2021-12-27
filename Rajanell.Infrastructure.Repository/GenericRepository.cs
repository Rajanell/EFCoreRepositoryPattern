using Microsoft.EntityFrameworkCore;
using Rajanell.Core.Model.Common;
using Rajanell.Core.Repository;
using Rajanell.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SoccerLeagueDBContext _context;
        private readonly DbSet<T> entity = null;

        public GenericRepository(SoccerLeagueDBContext context)
        {
            _context = context;
            entity = _context.Set<T>();
        }

        public async Task Add(T item)
        {
            await _context.AddAsync(item);
        }

        public async Task Add(IEnumerable<T> items)
        {
            await _context.AddRangeAsync(items);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> query)
        {
            return await entity.Where(query).ToListAsync();
        }

        public async Task<PagedList<T>> Find(Expression<Func<T, bool>> query, ResourceParameters resourceParameters)
        {
            var collection = entity.Where(query) as IQueryable<T>;
            return await Task.Run(() => PagedList<T>.Create(collection, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entity.ToListAsync();
        }

        public async Task<PagedList<T>> GetAll(ResourceParameters resourceParameters)
        {
            var collection = entity as IQueryable<T>;
            return await Task.Run(() => PagedList<T>.Create(collection, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<T> GetByID(Guid id)
        {
            return await entity.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T item)
        {
            entity.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Update(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                entity.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
