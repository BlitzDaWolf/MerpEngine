using MerpEngine;
using System;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Level l = new Level();
            Debug.Log(l.Save());

            Game.Start();
        }
    }
}
