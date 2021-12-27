using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rajanell.Core.Model;
using Rajanell.Core.Model.Common;
using Rajanell.Core.Repository.Read;
using Rajanell.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Infrastructure.Repository.Read
{
    public class StadiumQueryRepository : IStadiumQueryRepository
    {
        private readonly SoccerLeagueDBContext _context;

        public StadiumQueryRepository(SoccerLeagueDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stadium>> Find(Expression<Func<Stadium, bool>> query)
        {
            return await _context.Stadium.Where(query).ToListAsync();
        }

        public async Task<PagedList<Stadium>> Find(Expression<Func<Stadium, bool>> query, ResourceParameters resourceParameters)
        {
            var collection = _context.Stadium.Where(query);
            return await Task.Run(() => PagedList<Stadium>.Create(collection, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<IEnumerable<Stadium>> GetAll()
        {
            return await _context.Stadium.ToListAsync();
        }

        public async Task<PagedList<Stadium>> GetAll(ResourceParameters resourceParameters)
        {
            var collection = _context.Stadium;
            return await Task.Run(() => PagedList<Stadium>.Create(collection, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<Stadium> GetByID(Guid id)
        {
            return await _context.Stadium.FindAsync(id);
        }
    }
}
