using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Rajanell.Core.Repository.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rajanell.Core.Model;
using Rajanell.Core.Repository.Read;

namespace Rajanell.Test.TestCases
{
    [TestClass]
    public class CRUD
    {
        private IServiceProvider _serviceProvider;
        //command
        private ITeamCommandRepository _teamCommandRepository;
        private IStadiumCommandRepository _stadiumCommandRepository;
        private IPlayerCommandRepository _playerCommandRepository;
        //query
        private ITeamQueryRepository _teamQueryRepository;
        private IPlayerQueryRepository _playerQueryRepository;
        private IStadiumQueryRepository _stadiumQueryRepository;

        [TestInitialize]
        public void Initialize()
        {
            _serviceProvider = ServicesProvider.GetServiceProvider();
            _teamCommandRepository = _serviceProvider.GetService<ITeamCommandRepository>();
            _stadiumCommandRepository = _serviceProvider.GetService<IStadiumCommandRepository>();
            _playerCommandRepository = _serviceProvider.GetService<IPlayerCommandRepository>();

            _teamQueryRepository = _serviceProvider.GetService<ITeamQueryRepository>();
            _playerQueryRepository = _serviceProvider.GetService<IPlayerQueryRepository>();
            _stadiumQueryRepository = _serviceProvider.GetService<IStadiumQueryRepository>();
        }

        [TestMethod]
        public async Task CreateRecordsTest()
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
                players.Add(new Player
                {
                    PlayerId = Guid.NewGuid(),
                    TeamId = teamId,
                    Name = "Player " + i,
                    Position = (Position)randonPosition.Next(0, 4),
                    ShirtNumber = i
                });

            await _stadiumCommandRepository.Add(stadium);
            await _teamCommandRepository.Add(team);
            await _playerCommandRepository.Add(players);
            await _playerCommandRepository.Save();

            //check if data is inserted successfully
            var insertedStadium = await _stadiumQueryRepository.GetByID(stadiumId);
            var insertedTeam = await _teamQueryRepository.GetByID(teamId);
            var teamPlayers = await _playerQueryRepository.Find(x => x.TeamId == teamId);

            Assert.AreEqual(stadiumId, insertedStadium.StadiumId);
            Assert.AreEqual(teamId, insertedTeam.TeamId);
            Assert.IsTrue(teamPlayers.Any());
        }

    }
}
