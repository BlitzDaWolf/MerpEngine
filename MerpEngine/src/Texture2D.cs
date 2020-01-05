using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public struct Texture2D
    {
        private int id;
        private Vector2 size;

        public int ID => id;
        public Vector2 Size => size;
        public int Width => (int)size.X;
        public int Heigth => (int)size.Y;

        public Texture2D(int id, Vector2 size)
        {
            this.id = id;
            this.size = size;
        }
    }
}
