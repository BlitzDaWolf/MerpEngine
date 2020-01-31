using MerpEngine;
using OpenTK;
using System;

namespace MerpEngineExample.Compoments
{
    [Serializable]
    public class CameraMovement : Compoment
    {
        public float Speed { get; set; } = 2f;

        public override void Start()
        {
            Debug.Log("Hello world!");
            DebugConsole.AddCommand("speed", (args) =>
            {
                int s = 0;
                if(int.TryParse(args[0], out s))
                {
                    Speed = s;
                }
                else
                {
                    Debug.Log("fail");
                }
            });
            DebugConsole.AddCommand("zoom", setZoom);
        }

        public void setZoom(string[] args)
        {
            float f = 0;
            if (args.Length >= 1)
            {
                if (float.TryParse(args[0], out f))
                {
                    Camera.Main.zoom = f;
                }
            }
        }

        public override void Update()
        {
            Vector2 speed = AxiesManager.Get2DAxies("Movement") * this.Speed;

            if (Input.KeyPress(OpenTK.Input.Key.Q))
            {
                Application.Quit();
            }

            if (speed != Vector2.Zero)
            {
                Camera.Main.Translate(speed);
            }
        }
    }
}
