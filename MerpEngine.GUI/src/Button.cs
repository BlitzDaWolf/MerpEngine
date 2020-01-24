using MerpEngine.Renderes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngine.GUI
{
    public class Button : UI
    {
        public Sprite Default;
        public Sprite OnHover;
        public Sprite OnClick;

        public override void Start()
        {
            base.sprite = Default;
        }

        public override void Update()
        {
            var handeler = LevelManager.Levels[LevelManager.loadedLevel].GameObjects.Find(x => x.Name == "EventHandeler").GetCompoment<EventHandeler>();
            if (handeler.isHovering(this))
            {
                if (OnHover != null)
                {
                    base.sprite = OnHover;
                }
            }
            else
            {
                base.sprite = Default;
            }
        }

        public override void Render()
        {

        }
    }
}
