using MerpEngine;
using System;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Start();
            LevelManager.Peek();
            LevelManager.LoadLevel(1);
            LevelManager.Peek();
        }
    }
}
