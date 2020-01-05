using OpenTK;
using System.Collections.Generic;

namespace MerpEngine.GUI
{
    public class EventHandeler
    {
        public static EventHandeler Instance;

        public List<UI> UIEllements { get; private set; } = new List<UI>();

        List<UI> hoveredMaterials = new List<UI>();
        List<UI> lastHoveredMaterials = new List<UI>();

        public bool UIHover { get; private set; }
        public bool UIPressed { get; private set; }

        public EventHandeler()
        {
            Instance = this;
        }

        public void Update()
        {
            Vector2 mousePosition = Input.MousePosition;
            foreach (var item in UIEllements)
            {
                if (HoveringOver(item))
                {
                    item.OnHover();
                    hoveredMaterials.Add(item);
                    if (item is Button)
                    {
                        if (Input.MouseButtonPress(OpenTK.Input.MouseButton.Left))
                        {
                            Debug.Log("This button has been pressed");
                        }
                    }
                }
            }
            foreach (var item in lastHoveredMaterials)
            {
                if (!hoveredMaterials.Contains(item))
                {
                    item.NoHover();
                }
            }
            lastHoveredMaterials = hoveredMaterials;
            hoveredMaterials = new List<UI>();
        }

        public bool HoveringOver(UI ui)
        {
            Vector2 topLeft = new Vector2(ui.X, ui.Y);
            Vector2 bottomRigth = new Vector2(ui.X + ui.Width, ui.Y + ui.Heigth);

            if (topLeft.X < Input.MousePosition.X &&
                topLeft.Y < Input.MousePosition.Y &&

                bottomRigth.X > Input.MousePosition.X &&
                bottomRigth.Y > Input.MousePosition.Y)
            {
                return true;
            }
            return false;
        }

        internal void Render() => UIEllements.ForEach(x => x.render(this));
    }
}
