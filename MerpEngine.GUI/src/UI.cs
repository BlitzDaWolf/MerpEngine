using MerpEngine.Compoments;
using OpenTK;
using System;
using System.Drawing;
using System.Xml;
using Rectangle = System.Drawing.Rectangle;

namespace MerpEngine.GUI
{
    public enum AnchorPoints
    {
        TopLeft = 0,
        TopRight = 1,

        BottomLeft = 2,
        BottomRight = 3
    }

    [Serializable]
    public class UI : SpriteCompoment
    {
        public AnchorPoints Anchor;
        public Rectangle Rect;

        public UI()
        {

        }

        public UI(XmlNode node)
        {
            var go = new GameObject();
            go.Compoments.Add(this);
            this.GameObject = go;

            var atrs = node.Attributes;

            if (atrs != null)
            {
                var rectAtr = node.Attributes.GetNamedItem("rect");
                var anchorAtr = node.Attributes.GetNamedItem("anchor");

                if(rectAtr != null)
                {
                    {
                        string[] rectSplited = rectAtr.Value.Split(new char[] { ',' });
                        if(rectSplited.Length >= 4)
                        {
                            int x, y, w, h;
                            int.TryParse(rectSplited[0], out x);
                            int.TryParse(rectSplited[1], out y);
                            int.TryParse(rectSplited[2], out w);
                            int.TryParse(rectSplited[3], out h);

                            Rect = new Rectangle(x, y, w, h);
                        }
                        else
                        {
                            Debug.Log("Can't create rect");
                        }
                    }
                }
                if(anchorAtr != null)
                {

                }
            }
            else
            {
                Debug.Log(node.Attributes);
            }
        }

        public override void Start()
        {
            RenderIndex += 900;
        }

        public override void Render()
        {
            if (sprite == null) return;
            if (sprite.Material == null) return;

            Vector2 position = new Vector2(Rect.X, Rect.Y);

            Vector2 size = new Vector2(Screen.Width / 2, Screen.Heigth / 2);
            SpriteBatch.Draw(sprite.Material.texture,
                (Camera.Main.Position - size), Vector2.One, Vector2.Zero);
        }
    }
}
