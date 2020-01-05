using System;

namespace MerpEngine
{
    [Serializable]
    public class Compoment
    {
        public virtual void Update() { }
        public virtual void Destroy() { }
        public virtual void Start() { }
    }
}
