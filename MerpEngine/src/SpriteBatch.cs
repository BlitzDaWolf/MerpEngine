
using OpenTK;
using OpenTK.Graphics.OpenGL;
using RectangleF = System.Drawing.RectangleF;

namespace MerpEngine
{
    public class SpriteBatch
    {
        public static void Draw(
            Texture2D texture,
            Vector2 position,
            Vector2 cale,
            Vector2 origin,
            RectangleF? sourceRec = null)
        {
            Vector2[] vericies = new Vector2[4]
            {
                new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(1,1),
                new Vector2(0,1)
            };

            GL.BindTexture(TextureTarget.Texture2D, texture.ID);

            GL.Begin(PrimitiveType.Quads);
            for (int i = 0; i < 4; i++)
            {
                if (sourceRec == null)
                    GL.TexCoord2(vericies[i]);
                else
                {
                    GL.TexCoord2(
                        (sourceRec.Value.Left + vericies[i].X * sourceRec.Value.Width) / texture.Width, // X
                        (sourceRec.Value.Top + vericies[i].Y * sourceRec.Value.Height) / texture.Heigth); // Y
                }

                vericies[i].X *= (sourceRec == null) ? texture.Width : sourceRec.Value.Width;
                vericies[i].Y *= (sourceRec == null) ? texture.Heigth : sourceRec.Value.Height;
                vericies[i] -= origin;
                vericies[i] *= cale;
                vericies[i] += position;

                GL.Vertex2(vericies[i]);
            }
            GL.End();
        }

        public static void Begin(int width, int heigth)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(-width / 2f, width / 2f, heigth / 2f, -heigth / 2f, 0f, 1f);
        }
    }
}
