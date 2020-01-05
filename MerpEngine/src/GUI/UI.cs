using OpenTK;
using System;
using System.Drawing;
using System.Linq;
using Rectangle = System.Drawing.Rectangle;

namespace Engine.GUI
{
    public class UI
    {
        private Rectangle position;

        Material BaseMaterial;

        private int x;
        private int y;
        private int width;
        private int heigth;

        public int X {
            get => position.X;
            set
            {
                x = value;
                UpdatePosition();
            }
        }
        public int Y {
            get => position.Y;
            set
            {
                y = value;
                UpdatePosition();
            }
        }

        public int Width {
            get => position.Width;
            set
            {
                width = value;
                UpdatePosition();
            }
        }
        public int Heigth {
            get => position.Height;
            set
            {
                heigth = value;
                UpdatePosition();
            }
        }
        public Rectangle Position => position;

        public Material Material = Material.Materials.FirstOrDefault().Value;
        public Material OnHoverMat;

        private void UpdatePosition()
        {
            position = new Rectangle(
                x,
                y,
                width,
                heigth);
        }

        public UI()
        {

        }
        public UI(int x,int y, int width, int heigth)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.heigth = heigth;

            UpdatePosition();
        }
        public UI(Vector2 position, Vector2 scale) : this((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y) { }
        public UI(Rectangle position) : this(position.X, position.Y, position.Width, position.Height) { }


        internal void OnHover()
        {
            if (OnHoverMat != null)
            {
                if (BaseMaterial == null)
                {
                    BaseMaterial = Material;
                }
                Material = OnHoverMat;
            }
        }

        internal void NoHover()
        {
            Material = BaseMaterial;
        }

        internal virtual void render(EventHandeler eventHandeler)
        {
            if (Material == null)
                return;
            SpriteBatch.Draw(
                Material.texture,
                View.GameCamera.Position - (new Vector2(Screen.Width, Screen.Heigth) / 2) + new Vector2(X, Y),
                new Vector2(width / Material.texture.Size.X, heigth / Material.texture.Size.Y),
                Vector2.Zero);
        }
    }
}
