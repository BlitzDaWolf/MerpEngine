
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
            Vector2 scale,
            Vector2 origin,
            int index = -2,
            RectangleF? sourceRec = null)
        {
            Vector3[] vericies = new Vector3[4]
            {
                new Vector3(0, 0, index),
                new Vector3(1, 0, index),
                new Vector3(1, 1, index),
                new Vector3(0, 1, index)
            };

            GL.BindTexture(TextureTarget.Texture2D, texture.ID);

            GL.Begin(PrimitiveType.Quads);
            for (int i = 0; i < 4; i++)
            {
                if (sourceRec == null)
                    GL.TexCoord2(vericies[i].X, vericies[i].Y);
                else
                {
                    GL.TexCoord2(
                        (sourceRec.Value.Left + vericies[i].X * sourceRec.Value.Width) / texture.Width, // X
                        (sourceRec.Value.Top + vericies[i].Y * sourceRec.Value.Height) / texture.Heigth); // Y
                }

                vericies[i].X *= (sourceRec == null) ? texture.Width : sourceRec.Value.Width;
                vericies[i].Y *= (sourceRec == null) ? texture.Heigth : sourceRec.Value.Height;
                vericies[i] -= new Vector3(origin);
                vericies[i] *= new Vector3(scale);
                vericies[i] += new Vector3(position);

                GL.Vertex2(vericies[i].X, vericies[i].Y);
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
