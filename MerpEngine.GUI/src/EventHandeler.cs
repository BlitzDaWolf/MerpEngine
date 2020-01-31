using MerpEngine.Compoments;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngine.GUI
{
    public class EventHandeler : SpriteCompoment
    {
        public override void Start()
        {
            base.Start();
            base.GameObject.Name = "EventHandeler";
            GameObject.DontDestroyOnLoad();
        }

        public override void Update() { }

        public bool isHovering(UI ui)
        {
            Vector2 mousePosition = Input.MousePosition;

            Vector2 topLeft = new Vector2(ui.UISize.Top, ui.UISize.Left);
            Vector2 bottomRigth = new Vector2(ui.UISize.Right, ui.UISize.Bottom);

            return (mousePosition.X < topLeft.X && mousePosition.Y < topLeft.Y) && (mousePosition.X > bottomRigth.X && mousePosition.Y > bottomRigth.Y);
        }

        public override void Render() { }
    }
}
