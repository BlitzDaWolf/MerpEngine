using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

using System.Diagnostics;
using MerpEngine.Renderes;

namespace MerpEngine.GUI.src
{
    [Serializable]
    public class UIText : UI
    {
        #region Public data
        public string Text = "test";
        public string Font = "Arial";

        public int Size = 64;

        public int Width = 800;
        public int Heigth = 600;
        public Color color = Color.Black;

        public bool ResizeWithScreen = true;
        #endregion

        #region Private data
        public string _Text;
        public string _Font = "Arial";

        public int _Size = 16;

        public int _Width = 800;
        public int _Heigth = 600;
        public Color _color = Color.Black;
        #endregion

        public bool Changed { get; private set; } = true;

        private Font _font => new Font(Font, Size);

        private Bitmap GenerateText()
        {
            Bitmap bmp = new Bitmap(Width, Heigth, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics gfx = Graphics.FromImage(bmp);

            SolidBrush drawBrush = new SolidBrush(color);

            StringFormat drawFormat = new StringFormat();

            gfx.DrawString(Text, _font, drawBrush, 0, 0, drawFormat);
            gfx.Dispose();

            bmp.Save("test.png");
            return bmp;
        }

        public void Check()
        {
            if (
                (Text != _Text) ||
                (Width != _Width) ||
                (Heigth != _Heigth) ||
                (color != _color)
                )
            {
                _Text = Text;
                _Width = Width;
                _Heigth = Heigth;
                _color = color;
               Changed = true;
            }
        }

        public override void Update()
        {
            if (ResizeWithScreen)
            {
                Width = Screen.Width;
                Heigth = Screen.Heigth;
            }

            Text = $"FPS {(int)Frame.avg}";

            Check();
            if (Changed)
            {
                Texture2D tex2D = ContentPipe.LoadTexture(GenerateText());

                Sprite spr = new Sprite() { Material = new Material(tex2D) };
                sprite = spr;

                Changed = false;
            }
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
