using MerpEngine;
using MerpEngine.Compoments;
using MerpEngine.GUI;
using MerpEngine.GUI.src;
using System;

namespace MerpEngineExample
{
    [Serializable]
    public class TestCompoment : Compoment
    {
        public string test = "Yeet";

        UIText t;
        UIText tim;

        public override void Start()
        {
            GameObject go = new GameObject();
            EventHandeler eh = go.AddCompoment<EventHandeler>();
            eh.Load("data/UI/UI1.xml");

            t = eh.GetUI<UIText>("test");
            tim = eh.GetUI<UIText>("timer");
        }

        public override void Update()
        {
            tim.Text = $"{DateTime.Now}\n{DateTime.Now}";
        }
    }
}
