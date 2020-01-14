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
        }

        public override void Update()
        {
            Debug.Log(Camera.Main.Position);
            float horizontal = 0;
            float vertical = 0;

            if (Input.KeyDown(OpenTK.Input.Key.W))
            {
                horizontal = 1;
            }
            else if (Input.KeyDown(OpenTK.Input.Key.S))
            {
                horizontal = -1;
            }

            if (Input.KeyDown(OpenTK.Input.Key.D))
            {
                vertical = 1;
            }
            else if (Input.KeyDown(OpenTK.Input.Key.A))
            {
                vertical = -1;
            }

            Vector2 speed = new Vector2(horizontal, vertical);
            Camera.Main.SetPosition(Camera.Main.Position + (speed * Time.DeltaTime));
        }
    }
}
