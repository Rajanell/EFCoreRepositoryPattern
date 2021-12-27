using Microsoft.EntityFrameworkCore;
using Rajanell.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Infrastructure.Data
{
    public class SoccerLeagueDBContext : DbContext
    {
        public SoccerLeagueDBContext(DbContextOptions<SoccerLeagueDBContext> dbco) : base(dbco) 
        { 
        }

        public DbSet<Team> Team { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Stadium> Stadium { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
