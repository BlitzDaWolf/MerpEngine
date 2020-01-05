using System;
using System.Collections.Generic;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Engine
{
    public class View
    {
        public static View GameCamera;

        private Vector2 position;

        public double rotation;
        public double zoom;

        public Vector2 Position => position;

        public View(Vector2 startPosition, double startZoom = 1, double startRotation = 0)
        {
            this.position = startPosition;
            this.rotation = startRotation;
            this.zoom = startZoom;

            GameCamera = this;
        }

        public Vector2 ToWorld(Vector2 input)
        {
            input /= (float)zoom;
            Vector2 dX = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            Vector2 dY = new Vector2((float)Math.Cos(rotation + MathHelper.PiOver2), (float)Math.Sin(rotation + MathHelper.PiOver2));

            return (position + dX * input.X + dY * input.Y) - new Vector2(Screen.Width, Screen.Heigth) / 2;
        }

        public void Update()
        {
            float x = 0;
            float y = 0;
            if (Input.KeyDown(OpenTK.Input.Key.W))
            {
                y = -1;
            }
            else if (Input.KeyDown(OpenTK.Input.Key.S))
            {
                y = 1;
            }

            if (Input.KeyDown(OpenTK.Input.Key.D))
            {
                x = 1;
            }
            else if (Input.KeyDown(OpenTK.Input.Key.A))
            {
                x = -1;
            }

            Vector2 movement = (new Vector2(x, y) * 100) * Time.DeltaTime;
            SetPosition(position + movement);
        }
        public void SetPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }
        public Matrix4 ProjectionMatrix()
        {
            Matrix4 transform = Matrix4.Identity;

            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ((float)-rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)zoom, (float)zoom, 1.0f));

            return transform;
        }
        public void ApplyTransform()
        {
            Matrix4 transform = ProjectionMatrix();/* Matrix4.Identity;

            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ((float)-rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)zoom, (float)zoom, 1.0f));*/

            GL.MultMatrix(ref transform);
        }
    }
}
