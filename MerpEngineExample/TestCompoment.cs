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
        private bool succes;

        UIText t;

        public override void Start()
        {
            GameObject go = new GameObject();
            EventHandeler eh = go.AddCompoment<EventHandeler>();
            eh.Load("data/UI/UI1.xml");

            t = eh.GetUI<UIText>("test");
        }

        public override void Update()
        {
            if (t != null)
                t.Text = Frame.FPS.ToString();
        }
    }
}
