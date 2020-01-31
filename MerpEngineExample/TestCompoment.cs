using MerpEngine;
using MerpEngine.Compoments;
using MerpEngine.GUI;
using MerpEngine.GUI.src;
using System;

namespace MerpEngineExample
{
    [Serializable]
    public class TestCompoment : Compoment
    {
        public string test = "Yeet";
        private bool succes;

        public override void Start()
        {
            GameObject go = new GameObject();
            go.AddCompoment<EventHandeler>();
        }

        public override void Update()
        {
            if(succes == false)
            {
                try
                {
                    Random rnd = new Random();
                    for (int x = 0; x < 100; x++)
                    {
                        for (int y = 0; y < 100; y++)
                        {
                            GameObject go = new GameObject();
                            go.Position = new OpenTK.Vector2(x, y) * 128;

                            go.Active = rnd.Next(0, 100) > 50;
                            
                            if (y % 2 == 1)
                            {
                                if (x % 2 == 1)
                                {
                                    go.AddCompoment<SpriteCompoment>().sprite = new MerpEngine.Renderes.Sprite() { Material = Material.Materials["test"], sizePerPixel = 128 };
                                }
                                else
                                {
                                    go.AddCompoment<SpriteCompoment>().sprite = new MerpEngine.Renderes.Sprite() { Material = Material.Materials["test2"], sizePerPixel = 128 };
                                }
                            }
                            else
                            {

                                if (x % 2 == 0)
                                {
                                    go.AddCompoment<SpriteCompoment>().sprite = new MerpEngine.Renderes.Sprite() { Material = Material.Materials["test"], sizePerPixel = 128 };
                                }
                                else
                                {
                                    go.AddCompoment<SpriteCompoment>().sprite = new MerpEngine.Renderes.Sprite() { Material = Material.Materials["test2"], sizePerPixel = 128 };
                                }
                            }
                            LevelManager.LoadedLevel.GameObjects.Add(go);
                        }
                    }
                    succes = true;
                }
                catch (Exception)
                {
                    Debug.Log("Err");
                }
            }
        }
    }
}
