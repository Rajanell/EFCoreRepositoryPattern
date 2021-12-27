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
    public class PlayerQueryRepository : IPlayerQueryRepository
    {
        private readonly SoccerLeagueDBContext _context;

        public PlayerQueryRepository(SoccerLeagueDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> Find(Expression<Func<Player, bool>> query)
        {
            return await _context.Player.Include(x => x.Team).Where(query).ToListAsync();
        }

        public async Task<PagedList<Player>> Find(Expression<Func<Player, bool>> query, ResourceParameters resourceParameters)
        {
            var collection = _context.Player.Where(query);
            return await Task.Run(() => PagedList<Player>.Create(collection, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            return await _context.Player.Include(x => x.Team).ToListAsync();
        }

        public async Task<PagedList<Player>> GetAll(ResourceParameters resourceParameters)
        {
            var collection = _context.Player.Include(x => x.Team);
            return await Task.Run(() => PagedList<Player>.Create(collection, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<Player> GetByID(Guid id)
        {
            return await Task.Run(() => _context.Player.Include(x => x.Team).FirstOrDefault(x => x.TeamId == id));
        }
    }
}
