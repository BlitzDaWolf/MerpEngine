using MerpEngine;
using MerpEngine.Compoments;
using MerpEngineExample.Compoments;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ContentPipe.LoadInput();
            AxiesManager.instance.Axies2D = new System.Collections.Generic.Dictionary<string, Axies2D>();
            AxiesManager.instance.Axies = new System.Collections.Generic.Dictionary<string, Axies>();
            
            AxiesManager.instance.Axies2D.Add("Movement", new Axies2D() { Horizontal = "Horizontal", Vertical = "Vertical" });

            AxiesManager.instance.Axies.Add("Horizontal", new Axies() { PositiveKey = OpenTK.Input.Key.D, NegitiveKey = OpenTK.Input.Key.A });
            AxiesManager.instance.Axies.Add("Vertical", new Axies() { PositiveKey = OpenTK.Input.Key.W, NegitiveKey = OpenTK.Input.Key.S, Inverted = true });
            AxiesManager.instance.Axies.Add("Escape", new Axies() { PositiveKey = OpenTK.Input.Key.Escape });
            ContentPipe.SaveInput();

            Level l = new Level();

            /*ContentPipe.LoadMaterial("test", "test.png", (mat) => {

                {
                    GameObject go = new GameObject();
                    go.Position = new OpenTK.Vector2(0, 0);
                    var sc = go.AddCompoment<SpriteCompoment>();
                    sc.RenderIndex = 0;
                    sc.sprite = new MerpEngine.Renderes.Sprite() { Material = mat, sizePerPixel = 128 };
                    l.GameObjects.Add(go);
                }

                ContentPipe.SaveLevel(l, "levels/level1.lvl");
            });
            ContentPipe.LoadMaterial("test2", "test2.png", (mat) => {

                {
                    GameObject go = new GameObject();
                    go.Position = new OpenTK.Vector2(0.5f, 0.5f);
                    var sc = go.AddCompoment<SpriteCompoment>();
                    sc.RenderIndex = 1;
                    sc.sprite = new MerpEngine.Renderes.Sprite() { Material = mat, sizePerPixel = 128 };
                    l.GameObjects.Add(go);
                }

                ContentPipe.SaveLevel(l, "levels/level1.lvl");
            });

            {
                GameObject go = new GameObject();
                go.AddCompoment<MerpEngine.GUI.src.UIText>();
                go.AddCompoment<CameraMovement>();
                l.GameObjects.Add(go);
                ContentPipe.SaveLevel(l, "levels/level1.lvl");
            }*/
            Arguments.SetEnviroments(args);

            Game.Start();
        }
    }
}
