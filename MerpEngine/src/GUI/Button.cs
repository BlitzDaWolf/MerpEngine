using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace Engine.GUI
{
    public class Button : UI
    {
        public bool Active = true;

        public Material OnPressNat;

        public Action OnPress;

        public Button()
        {

        }
        public Button(int x, int y, int width, int heigth) : base(x, y, width, heigth) { }
        public Button(Vector2 position, Vector2 scale) : this((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y) { }
        public Button(Rectangle position) : this(position.X, position.Y, position.Width, position.Height) { }

        internal void Press()
        {
            if (Active)
            {
                OnPress.Invoke();
            }
        }
    }
}
