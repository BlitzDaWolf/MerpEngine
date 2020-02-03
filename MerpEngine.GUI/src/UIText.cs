using System;
using System.Linq;
using System.Drawing;
using System.Xml;
using MerpEngine.Renderes;
using System.Globalization;

namespace MerpEngine.GUI
{
    [Serializable]
    public class UIText : UI
    {
        #region Public data
        public string Text{
            get => _Text;
            set {
                if(_Text != value){
                    Changed = true;
                    _Text = value;
                }
            }
        }
        public string Font{
            get => _Font;
            set {
                if(_Font != value){
                    Changed = true;
                    _Font = value;
                }
            }
        }

        public int Size{
            get => _Size;
            set{
                if(_Size != value){
                    Changed = true;
                    _Size = value;
                }
            }
        }

        public Color color{
            get => _color;
            set{
                if(_color != value){
                    Changed = true;
                    _color = value;
                }
            }
        }
        #endregion

        #region Private data
        private string _Text;
        private string _Font = "Arial";

        private int _Size = 16;

        public Color _color = Color.Black;

        private Bitmap oldBitmap;
        #endregion

        public UIText()
        {
            Rect = new Rectangle(0, 0, 800, 600);
        }

        #region XMLLoading
        public UIText(XmlNode node) : base(node)
        {
            Text = node.InnerText;
            for (int i = 0; i < node.Attributes.Count; i++)
            {
                var atr = node.Attributes.Item(i);
                if(atr.Name == "")
                {

                }
                switch (atr.Name)
                {
                    case "font":
                        SetXMLFont(atr.Value);
                        break;
                    case "font_size":
                        SetXMLSize(atr.Value);
                        break;
                    case "color":
                        SetXMLColor(atr.Value);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetXMLFont(string value) => Font = value;
        private void SetXMLSize(string value) => int.TryParse(value, out _Size);
        private void SetXMLColor(string value)
        {
            if(value[0] == '#')
            {
                int argb = Int32.Parse("FF" + value.Replace("#", ""), NumberStyles.HexNumber);
                color = Color.FromArgb(argb);
            }
            else
            {
                color = Color.FromName(value);
            }
        }
        #endregion

        public bool Changed { get; private set; } = true;

        private Font _font => new Font(Font, Size);

        private Bitmap GenerateText()
        {
            if (oldBitmap != null)
                oldBitmap.Dispose();
            Bitmap bmp = new Bitmap(Rect.Width, Rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics gfx = Graphics.FromImage(bmp);
            SolidBrush drawBrush = new SolidBrush(color);
            StringFormat drawFormat = new StringFormat();

            gfx.DrawString(Text, _font, drawBrush, 0, 0, drawFormat);
            gfx.Dispose();

            oldBitmap = bmp;

            return bmp;
        }

        public override void Update()
        {
            if(LevelManager.LoadedLevel.GetGameObjectsWithType<EventHandeler>()[0].GetCompoment<EventHandeler>().isHovering(this)){
                color = Color.Red;
            }else{
                color = Color.Green;
            }

            if (Changed)
            {
                if (sprite != null)
                {
                    ContentPipe.DisposeTexture(sprite.Material.texture.ID);
                }

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
