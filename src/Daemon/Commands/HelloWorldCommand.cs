using System;
using Coin.Daemon.Internal.Interfaces;
using DBreeze;

namespace Coin.Daemon.Commands
{
    public class HelloWorldCommand : IRunnable
    {
        public readonly DBreezeEngine _engine;
        public HelloWorldCommand(DBreezeEngine engine)
        {
            _engine = engine;
        }

        public void Run()
        {
            Console.WriteLine("Hello World!");
        }
    }
}