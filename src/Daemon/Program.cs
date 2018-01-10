using System;
using System.IO;
using Coin.Daemon.Internal.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coin.Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            StartUpProject<Startup>();
        }

        private static void PrepareDataForFirstRun()
        {
            if(!Directory.Exists(Configuration.DataPath)) 
                Directory.CreateDirectory(Configuration.DataPath);
            if(!File.Exists(Configuration.ConfigurationFilePath))
                File.Create(Configuration.ConfigurationFilePath).Close();
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Configuration.BasePath)
                .AddJsonFile(Configuration.ConfigurationFileName, optional: true, reloadOnChange: true)
                .Build();
        }

        private static void StartUpProject<TStartup>() where TStartup : IStartup
        {
            PrepareDataForFirstRun();
            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfiguration>(cfg => GetConfiguration());
            serviceCollection.AddSingleton(typeof(IStartup), typeof(TStartup));
            
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var startup = serviceProvider.GetService<IStartup>();
            var startupServices = new ServiceCollection();
            
            startup.ConfigureServices(startupServices);
            startup.Run(startupServices.BuildServiceProvider());
        }
    }
}
