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
        }

        public override void Update()
        {
            Vector2 speed = AxiesManager.Get2DAxies("Movement");

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
