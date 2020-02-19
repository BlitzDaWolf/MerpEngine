using OpenTK;

namespace MerpEngine.Physics
{
    public class Collider : Compoment
    {
        public float raduis;

        public virtual bool Colliding(Collider other) => Vector2Extension.Distance(GameObject.Position, other.GameObject.Position) >= raduis + other.raduis;
    }
}