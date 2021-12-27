using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rajanell.Core.Repository;
using Rajanell.Core.Repository.Read;
using Rajanell.Core.Repository.Write;
using Rajanell.Infrastructure.Data;
using Rajanell.Infrastructure.Repository;
using Rajanell.Infrastructure.Repository.Read;
using Rajanell.Infrastructure.Repository.Write;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Test.TestCases
{
    public static class ServicesProvider
    {
        public static IServiceProvider GetServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration config = builder.Build();

            var con = config.GetConnectionString("Default");

            services.AddDbContext<SoccerLeagueDBContext>(e => { e.UseSqlServer(config.GetConnectionString("Default")); });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            services.AddScoped<ITeamCommandRepository, TeamCommandRepository>();
            services.AddScoped<IStadiumCommandRepository, StadiumCommandRepository>();
            services.AddScoped<IPlayerCommandRepository, PlayerCommandRepository>();

            services.AddScoped<ITeamQueryRepository, TeamQueryRepository>();
            services.AddScoped<IStadiumQueryRepository, StadiumQueryRepository>();
            services.AddScoped<IPlayerQueryRepository, PlayerQueryRepository>();
            

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            var container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container);

            return serviceProvider;
        }
    }
}
