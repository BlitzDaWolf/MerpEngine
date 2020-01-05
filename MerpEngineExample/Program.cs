using MerpEngine;
using System;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments.SetEnviroments(args);

            Game.Start();
        }
    }
}
