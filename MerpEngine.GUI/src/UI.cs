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

        public Canvas Canvas { get; internal set; }

        public event EventHandler HoverEnter;
        public event EventHandler HoverLeave;
        public event EventHandler Click;

        internal void OnHoverEnter() => HoverEnter?.Invoke(this, new EventArgs());
        internal void OnHoverLeave() => HoverLeave?.Invoke(this, new EventArgs());
        internal void OnClick() => Click?.Invoke(this, new EventArgs());

        public UI()
        {
            Start();
        }

        public UI(XmlNode node) : base()
        {
            RenderIndex += 900;
            var go = new GameObject();
            go.Compoments.Add(this);
            this.GameObject = go;

            var atrs = node.Attributes;

            if (atrs != null)
            {
                var rectAtr = node.Attributes.GetNamedItem("rect");
                var anchorAtr = node.Attributes.GetNamedItem("anchor");
                var nameAtr = node.Attributes.GetNamedItem("name");

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
                if(nameAtr != null)
                {
                    GameObject.Name = nameAtr.Value;
                }
            }
            else
            {
                Debug.Log(node.Attributes);
            }
        }

        public Vector2 toWorldPosition()
        {
            Vector2 position = new Vector2(Rect.X, Rect.Y);

            Vector2 size = new Vector2(Screen.Width / 2, Screen.Heigth / 2);
            Vector2 globalPosition = Vector2.Zero;

            if (Canvas != null)
            {
                globalPosition = Canvas.Position;
                size = Canvas.Size / 2;
            }
            else
            {
                globalPosition = Camera.Main.Position;
            }

            return (globalPosition - size) + position;
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
            Vector2 globalPosition = Vector2.Zero;
            if(Canvas != null)
            {
                globalPosition = Canvas.Position;
                size = Canvas.Size / 2;
            }
            else
            {
                globalPosition = Camera.Main.Position;
            }

            SpriteBatch.Draw(sprite.Material.texture,
                (globalPosition - size) + position, Vector2.One, Vector2.Zero);
        }
    }
}
