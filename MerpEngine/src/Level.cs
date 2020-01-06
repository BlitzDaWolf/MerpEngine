using MerpEngine.Renderes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MerpEngine
{
    [Serializable]
    public class Level
    {
        public string Name = "";
        public List<Material> SharedMaterials = new List<Material>();
        public List<Compoment> compoments = new List<Compoment>();
        [NonSerialized]
        private SpriteMap spriteMap = new SpriteMap() { GridSize = 64 };

        public List<Sprite> GetSprite(int x, int y) => spriteMap.GetSprite(x, y).OrderBy(i => i.RenderOrder).ToList();
        public void AddSprite(Sprite spr) => spriteMap.sprites.Add(spr);

        internal void Render() => spriteMap.Render();
        internal void Update() => compoments.ForEach(i => i.Update());

        public string Save() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
        internal void Destroy() => compoments.ForEach(x => x.Destroy());
        internal void Start()
        {
            Camera.Main.SetPosition(Vector2.Zero);
            compoments.ForEach(x => x.Start());
        }
    }
}
