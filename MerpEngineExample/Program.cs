using MerpEngine;
using MerpEngine.Compoments;
using MerpEngineExample.Compoments;
using System;
using System.Diagnostics;
using System.Threading;
using Debug = MerpEngine.Debug;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments.SetEnviroments(args);

            Stopwatch sw = Stopwatch.StartNew();
            ContentPipe.toLoadMaterial.Add("tt", new Tuple<string, Action<Material>>("test.png", (Material m) => {
                sw.Stop();
                Debug.Log(sw.ElapsedMilliseconds / 100);
            }));

            Game.Start();

            // ContentPipe.SaveLevel(LevelManager.Levels[0], "level1");
        }
    }
}
