using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rajanell.Core.Model;
using Rajanell.Core.Repository.Write;
using Rajanell.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Infrastructure.Repository.Write
{
    public class TeamCommandRepository : ITeamCommandRepository
    {
        private readonly SoccerLeagueDBContext _context;

        public TeamCommandRepository(SoccerLeagueDBContext context)
        {
            _context = context;
        }

        public async Task Add(Team item)
        {
            await _context.AddAsync(item);
        }

        public async Task Add(IEnumerable<Team> items)
        {
            await _context.AddRangeAsync(items);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Team item)
        {
            _context.Attach(item);
            _context.Entry(item).State = EntityState.Modified;

        }

        public void Update(IEnumerable<Team> items)
        {
            foreach(var item in items)
            {
                _context.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
