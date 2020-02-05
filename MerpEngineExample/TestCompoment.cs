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

        float timer = 10;

        public override void Start()
        {
            timer = 10;
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
            Random rn = new Random();
            score++;
            b.Rect = new System.Drawing.Rectangle(rn.Next(Screen.Width - 64), rn.Next(Screen.Heigth -64), 256 / score, 256 / score);
            timer += score * rn.Next(7);
            b.Scale = new OpenTK.Vector2(1f / score, 1f / score);
        }

        public override void Update()
        {
            if(Input.KeyPress(OpenTK.Input.Key.Space)){
                buttonClicked(null, new EventArgs());
            }

            if (t != null)
                t.Text = score.ToString();
            if(tim != null)
                tim.Text = timer.ToString();

            timer -= score * Time.DeltaTime;
            if(timer < 0){
                Debug.Log(score);
                Application.Quit();
            }
        }
    }
}
