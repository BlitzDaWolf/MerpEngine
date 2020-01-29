using MerpEngine.Compoments;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MerpEngine
{
    [Serializable]
    public class GameObject
    {
        internal Guid Id { get; set; }

        public GameObject parrent;

        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 GlobalPosition
        {
            get
            {
                if (parrent != null)
                    return parrent.GlobalPosition + Position;
                else
                    return Position;
            }
        }


        public List<Compoment> Compoments = new List<Compoment>();
        internal void Update() => Compoments.ForEach(i => i.Update());

        internal void Render()
        {
            var v = Compoments
                .Where(x =>
                    x is SpriteCompoment)
                .Select(x =>
                    (SpriteCompoment)x)
                .ToList();
            foreach (var item in v) item.Render();
        }

        internal void Start() => Compoments.ForEach(x => x.Start());

        public T AddCompoment<T>() where T : Compoment, new()
        {
            T newObject = new T();
            newObject.GameObject = this;
            Compoments.Add(newObject);
            return newObject;
        }

        public T GetCompoment<T>() where T : Compoment
        {
            return Compoments.Find(x => x is T) as T;
        }

        public bool HasCompoment<T>() where T : Compoment => GetCompoment<T>() != null;
    }
}
