using MerpEngine.Renderes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngine.Compoments
{
    public class SpriteCompoment : Compoment
    {
        public Sprite sprite;

        public void Render()
        {
            if (sprite == null) return;
            if (sprite.Material == null) return;
            SpriteBatch.Draw(sprite.Material.texture, (Position * sprite.sizePerPixel) + sprite.Position, Scale * sprite.Size, Vector2.Zero);
        }
    }
}
