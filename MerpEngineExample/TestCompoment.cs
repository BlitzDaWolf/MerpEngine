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

        int score = 0;

        UIText t;
        UIText tim;
        Button b;

        float timer = 0;

        public override void Start()
        {
            timer = 0;
            GameObject go = new GameObject();
            EventHandeler eh = go.AddCompoment<EventHandeler>();
            eh.Load("data/UI/UI1.xml");

            t = eh.GetUI<UIText>("test");
            tim = eh.GetUI<UIText>("timer");
            b = eh.GetUI<Button>("click");
            if(b != null)
                b.Click += buttonClicked;
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            score++;
        }

        public override void Update()
        {
            timer += (600f) * Time.DeltaTime;
            var ts = new TimeSpan(0,0,(int)timer);
            t.Text = ts.ToString();
            tim.Text = $"{DateTime.Now + ts}\n{DateTime.Now}\n{(DateTime.Now + ts) - DateTime.Now}";
        }
    }
}
