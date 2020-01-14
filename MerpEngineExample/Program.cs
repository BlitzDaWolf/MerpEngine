using MerpEngine;
using MerpEngine.Compoments;
using MerpEngineExample.Compoments;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
           /* GameObject go = new GameObject();
            go.Name = "test";

            Level l = new Level() { Name = "level 1" };
            go.Position = new OpenTK.Vector2(10, 0);
            go.AddCompoment<CameraMovement>();
            SpriteCompoment sc = go.AddCompoment<SpriteCompoment>();
            sc.meshName = "test";
            sc.pathName = "test.png";
            sc.LoadNewSprite("test", "test.png");
            l.GameObjects.Add(go);
            ContentPipe.SaveLevel(l, "levels/level1.lvl");*/

            Arguments.SetEnviroments(args);

            Game.Start();
        }
    }
}
