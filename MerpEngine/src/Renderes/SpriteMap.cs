using System.Collections.Generic;
using System.Linq;

namespace MerpEngine.Renderes
{
    public class SpriteMap
    {
        public static SpriteMap Grid;

        public int GridSize = 128;

        public List<Sprite> sprites = new List<Sprite>();

        public SpriteMap()
        {
            Grid = this;
        }

        internal List<Sprite> GetSprite(int x, int y) => sprites.Where(i => i.Position == new OpenTK.Vector2(x * GridSize, y * GridSize)).ToList();

        public void Render()
        {
            var v = sprites.OrderBy(x => x.RenderOrder).ToList();
            v.ForEach(x => x.Render());
        }
    }
}
