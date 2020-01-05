using OpenTK;
using Rectangle = System.Drawing.Rectangle;

namespace MerpEngine.GUI
{
    public class UIInput : UI
    {
        public UIInput()
        {

        }
        public UIInput(int x, int y, int width, int heigth) : base(x, y, width, heigth)
        {
        }
        public UIInput(Vector2 position, Vector2 scale) : this((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y) { }
        public UIInput(Rectangle position) : this(position.X, position.Y, position.Width, position.Height) { }

        internal override void render(EventHandeler eventHandeler)
        {
            base.render(eventHandeler);
        }
    }
}
