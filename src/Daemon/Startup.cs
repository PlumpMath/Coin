using System;
using Coin.Daemon.Commands;
using Coin.Daemon.Internal.Interfaces;
using DBreeze;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coin.Daemon
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(db => new DBreezeEngine(Daemon.Configuration.DataPath));
            services.AddScoped<HelloWorldCommand>();
        }

        public void Run(IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<HelloWorldCommand>().Run();
        }
    }
}