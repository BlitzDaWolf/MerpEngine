using MerpEngine.Renderes;
using System.Collections.Generic;
using System.Linq;

namespace MerpEngine
{
    public class Level
    {
        public string Name = "";
        public List<Material> SharedMaterials = new List<Material>();
        public List<Compoment> compoments = new List<Compoment>();
        private SpriteMap spriteMap = new SpriteMap() { GridSize = 64 };

        public List<Sprite> GetSprite(int x, int y) => spriteMap.GetSprite(x, y).OrderBy(i => i.RenderOrder).ToList();
        public void AddSprite(Sprite spr) => spriteMap.sprites.Add(spr);

        internal void Render() => spriteMap.Render();
        internal void Update() => compoments.ForEach(i => i.Update());

        public string Save() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
