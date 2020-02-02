using MerpEngine;
using MerpEngine.Compoments;
using MerpEngineExample.Compoments;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ContentPipe.LoadInput();

            RecreateLevel();

            DebugConsole.AddCommand("relevel", (a) => { RecreateLevel(); });
            DebugConsole.AddCommand("reinput", (a) => { RecreateInputs(); });

            Arguments.SetEnviroments(args);

            Game.Start();
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
                go.Position = new OpenTK.Vector2(0, 0);
                var sc = go.AddCompoment<SpriteCompoment>();
                sc.RenderIndex = 0;
                sc.sprite = new MerpEngine.Renderes.Sprite() { Material = new Material("test", "test.png"), sizePerPixel = 128 };
                l.GameObjects.Add(go);

                GameObject go2 = new GameObject();
                go2.Position = new OpenTK.Vector2(1, 0);
                var sc2 = go2.AddCompoment<SpriteCompoment>();
                sc2.RenderIndex = 0;
                sc2.sprite = new MerpEngine.Renderes.Sprite() { Material = new Material("test2", "test2.png"), sizePerPixel = 128 };
                l.GameObjects.Add(go2);
                go2.parrent = go;
            }

            {
                GameObject go = new GameObject();
                
                go.AddCompoment<CameraMovement>();
                go.AddCompoment<TestCompoment>();
                l.GameObjects.Add(go);
            }
            ContentPipe.SaveLevel(l, "levels/level1.lvl");
        }
    }
}
