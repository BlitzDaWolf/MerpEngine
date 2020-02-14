using MerpEngine;
using MerpEngine.Compoments;
using MerpEngine.Networking;
using MerpEngine.Networking.Interface;
using MerpEngineExample.Compoments;
using System;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Console.ReadLine().ToLower().StartsWith("s"))
            {
                IServer headless = new HeadlessServer();
                headless.Start();
            }
            else
            {
                RecreateLevel();

                ContentPipe.LoadInput();

                DebugConsole.AddCommand("relevel", (a) => { RecreateLevel(); });
                DebugConsole.AddCommand("reinput", (a) => { RecreateInputs(); });

                Arguments.SetEnviroments(args);

                Game.Start();
            }
        }

        static void RecreateInputs()
        {
            Debug.Log("Recreating input");
            AxiesManager.instance.Axies2D = new System.Collections.Generic.Dictionary<string, Axies2D>();
            AxiesManager.instance.Axies = new System.Collections.Generic.Dictionary<string, Axies>();

            AxiesManager.instance.Axies2D.Add("Movement", new Axies2D() { Horizontal = "Horizontal", Vertical = "Vertical" });

            AxiesManager.instance.Axies.Add("Horizontal", new Axies() { PositiveKey = OpenTK.Input.Key.D, NegitiveKey = OpenTK.Input.Key.A });
            AxiesManager.instance.Axies.Add("Vertical", new Axies() { PositiveKey = OpenTK.Input.Key.W, NegitiveKey = OpenTK.Input.Key.S, Inverted = true });
            AxiesManager.instance.Axies.Add("Escape", new Axies() { PositiveKey = OpenTK.Input.Key.Escape });
            ContentPipe.SaveInput();
        }

        static void RecreateLevel()
        {
            Debug.Log("Recreating level");
            Level l = new Level();

            {
                GameObject go = new GameObject();
                
                // go.AddCompoment<CameraMovement>();
                go.AddCompoment<TestCompoment>();
                go.AddCompoment<Client>();
                l.GameObjects.Add(go);
            }
            ContentPipe.SaveLevel(l, "data/levels/level1.lvl");
        }
    }
}
