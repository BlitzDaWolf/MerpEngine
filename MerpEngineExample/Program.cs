using MerpEngine;
using MerpEngineExample.Compoments;
using System;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments.SetEnviroments(args);

            Game.Start();
            LevelManager.Levels[0].compoments.Add(new CameraMovementCompoment());
            // ContentPipe.SaveLevel(LevelManager.Levels[0], "level1");
        }
    }
}
