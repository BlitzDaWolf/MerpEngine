using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace MerpEngine
{
    public class Camera
    {
        public static Camera Main;

        private Vector2 position;

        public double rotation;
        public double zoom;

        public Vector2 Position => position;

        public Camera(Vector2 startPosition, double startZoom = 1, double startRotation = 0)
        {
            this.position = startPosition;
            this.rotation = startRotation;
            this.zoom = startZoom;

            Main = this;
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
        }
        public void SetPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }
        public void Translate(Vector2 vector2)
        {
            position += vector2;
        }
        public Matrix4 ProjectionMatrix()
        {
            Matrix4 transform = Matrix4.Identity;

            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            // transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ((float)-rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)zoom, (float)zoom, 1.0f));

            return transform;
        }
        public void ApplyTransform()
        {
            Matrix4 transform = ProjectionMatrix();

            GL.MultMatrix(ref transform);
        }
    }
}
