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
    public class TeamQueryRepository : ITeamQueryRepository
    {
        private readonly SoccerLeagueDBContext _context;

        public TeamQueryRepository(SoccerLeagueDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> Find(Expression<Func<Team, bool>> query)
        {
            return await _context.Team.Include(x => x.Players).Include(x => x.Stadium).Where(query).ToListAsync();
        }

        public async Task<PagedList<Team>> Find(Expression<Func<Team, bool>> query, ResourceParameters resourceParameters)
        {
            var collection =_context.Team.Include(x => x.Players).Include(x => x.Stadium).Where(query);
            return await Task.Run(() => PagedList<Team>.Create(collection, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            return await _context.Team.Include(x => x.Players).Include(x=> x.Stadium).ToListAsync();
        }

        public async Task<PagedList<Team>> GetAll(ResourceParameters resourceParameters)
        {
            var collection = _context.Team.Include(x => x.Players).Include(x => x.Stadium);
            return await Task.Run(() => PagedList<Team>.Create(collection, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<Team> GetByID(Guid id)
        {
            return await Task.Run(() => _context.Team.Include(x => x.Players).Include(x => x.Stadium).FirstOrDefault(x=> x.TeamId == id));
        }
    }
}
