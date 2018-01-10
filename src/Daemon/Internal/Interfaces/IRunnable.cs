using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coin.Daemon.Internal.Interfaces
{
    internal interface IRunnable
    {
        void Run();
    }

    internal interface IRunnable<T1>
    {
        void Run(T1 param1);
    }

    internal interface IRunnable<T1, T2>
    {
        void Run(T1 param1);
    }
}