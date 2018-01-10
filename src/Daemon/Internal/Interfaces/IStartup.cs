using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coin.Daemon.Internal.Interfaces
{
    internal interface IStartup
    {
        void ConfigureServices(IServiceCollection services);
        void Run(IServiceProvider serviceProvider);
    }
}