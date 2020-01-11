using OpenTK;
using System;

namespace MerpEngine
{
    [Serializable]
    public class Compoment
    {
        public string Name = Guid.NewGuid().ToString();
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public float Rotation { get; set; } = 0;

        public virtual void Update() { }
        public virtual void Destroy() { }
        public virtual void Start() { }
    }
}
