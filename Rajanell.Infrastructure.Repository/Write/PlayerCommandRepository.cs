using Microsoft.Extensions.Logging;
using Rajanell.Core.Model;
using Rajanell.Core.Repository;
using Rajanell.Core.Repository.Write;
using Rajanell.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Infrastructure.Repository.Write
{
    public class PlayerCommandRepository : IPlayerCommandRepository
    {
        private readonly SoccerLeagueDBContext _context;
        //can also use generic repository
        private readonly IGenericRepository<Player> _playerRepository;

        public PlayerCommandRepository(SoccerLeagueDBContext context, IGenericRepository<Player> playerRepository)
        {
            _context = context;
            _playerRepository = playerRepository;
        }

        public async Task Add(Player item)
        {
            await _playerRepository.Add(item);
        }

        public async Task Add(IEnumerable<Player> items)
        {
            await _playerRepository.Add(items);
        }

        public async Task Save()
        {
            await _playerRepository.Save();
        }

        public void Update(Player item)
        {
            _playerRepository.Update(item);
        }

        public void Update(IEnumerable<Player> items)
        {
            _playerRepository.Update(items);
        }
    }
}
