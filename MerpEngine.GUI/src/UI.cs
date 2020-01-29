using MerpEngine.Compoments;
using OpenTK;
using System;
using System.Drawing;
using Rectangle = System.Drawing.Rectangle;

namespace MerpEngine.GUI
{
    [Serializable]
    public class UI : SpriteCompoment
    {
        public Rectangle UISize;
        public Rectangle UIPosition;

        public UI()
        {
            RenderIndex = int.MaxValue;
        }

        public override void Render()
        {
            if (sprite == null) return;
            if (sprite.Material == null) return;

            Vector2 size = sprite.Material.texture.Size;
            SpriteBatch.Draw(sprite.Material.texture, Camera.Main.Position - (new Vector2(size.X / 2, size.Y / 2)), Scale, Vector2.Zero);
        }
    }
}
