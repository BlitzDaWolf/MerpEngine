using OpenTK;
using System;
using System.IO;

namespace MerpEngine.Renderes
{
    [Serializable]
    public class Sprite
    {
        public int sizePerPixel = 256;
        public Vector2 Position;

        public int RenderOrder = 0;

        public Material Material;

        public Vector2 Size
        {
            get => new Vector2(sizePerPixel / Material.texture.Size.X, sizePerPixel / Material.texture.Size.Y);
        }

        public virtual void Render() => SpriteBatch.Draw(Material.texture, Position * sizePerPixel, Size, Vector2.Zero);

        public static Sprite LoadSprite(string path)
        {
            var name = Path.GetFileNameWithoutExtension(path);
            Material m;
            if(Material.Materials.ContainsKey(name)){
                m = Material.Materials[name];
            }
            else
            {
                var tex = ContentPipe.LoadTexture(path);
                m = new Material(name, path, tex);
            }
            Sprite s = new Sprite(){ Material = m, sizePerPixel = Math.Max(m.texture.Width, m.texture.Heigth) };
            return s;
        }
    }
}
