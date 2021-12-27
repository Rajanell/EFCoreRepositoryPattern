using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Rajanell.Core.Model;
using Rajanell.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Test.TestCases
{
    [TestClass]
    public class CRUDGenericRepository
    {
        private IGenericRepository<Team> _teamRepository;
        private IGenericRepository<Stadium> _stadiumRepository;
        private IGenericRepository<Player> _playerRepository;
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void Initialize()
        {
            _serviceProvider = ServicesProvider.GetServiceProvider();
            _teamRepository = _serviceProvider.GetService<IGenericRepository<Team>>();
            _stadiumRepository = _serviceProvider.GetService<IGenericRepository<Stadium>>();
            _playerRepository = _serviceProvider.GetService<IGenericRepository<Player>>();
        }

        [TestMethod]
        public async Task PupulateAllTablesTest()
        {
            //Stadium
            var stadiumId = Guid.NewGuid();
            var stadium = new Stadium { StadiumId = stadiumId, Name = "Stadium 1", Capacity = 50000 };
            //Team
            var teamId = Guid.NewGuid();
            var team = new Team { TeamId = teamId, Name = "Team 1", StadiumId = stadiumId };
            //Players
            Random randonPosition = new Random();
            var players = new List<Player>();
            for (int i = 1; i <= 15; i++)
                players.Add(new Player {
                    PlayerId = Guid.NewGuid(), TeamId = teamId, Name = "Player " + i, Position = (Position)randonPosition.Next(0, 4), ShirtNumber = i });

            await _stadiumRepository.Add(stadium);
            await _teamRepository.Add(team);
            await _playerRepository.Add(players);
            await _playerRepository.Save();

            //check if data is inserted successfully
            var insertedStadium = await _stadiumRepository.GetByID(stadiumId);
            var insertedTeam = await _teamRepository.GetByID(teamId);
            var teamPlayers = await _playerRepository.Find(x => x.TeamId == teamId);

            Assert.AreEqual(stadiumId, insertedStadium.StadiumId);
            Assert.AreEqual(teamId, insertedTeam.TeamId);
            Assert.IsTrue(teamPlayers.Any());
        }
    }

}

