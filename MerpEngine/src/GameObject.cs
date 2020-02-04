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
        private Level myLevel;
        internal Guid Id { get; set; }

        public GameObject parrent;

        public bool Active { get; set; } = true;

        public string Name { get; set; } = Guid.NewGuid().ToString();
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

        public GameObject()
        {
            if(LevelManager._LaodedLevel != null)
            {
                LevelManager._LaodedLevel.GameObjects.Add(this);
                Start();
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

        internal void Start()
        {
            if(LevelManager._LaodedLevel != null)
            {
                myLevel = LevelManager._LaodedLevel;
            }
            {
                var copyList = new List<Compoment>(Compoments);    
                copyList.ForEach(x => x.Start());
            }
        }

        public T AddCompoment<T>() where T : Compoment, new()
        {
            T newObject = new T();
            newObject.GameObject = this;
            Compoments.Add(newObject);
            if (LevelManager.startLevel)
                newObject.Start();
            return newObject;
        }

        public T GetCompoment<T>() where T : Compoment
        {
            return Compoments.Find(x => x is T) as T;
        }
        public T[] GetCompoments<T>() where T : Compoment
        {
            return Compoments
                .Where(x => x is T)
                .Select(x => x as T)
                .ToArray();
        }

        public bool HasCompoment<T>() where T : Compoment => GetCompoment<T>() != null;

        public void DontDestroyOnLoad()
        {
            if(myLevel != null)
            {
                if(myLevel.Name != "Don't destroy on load")
                {
                    LevelManager.Dontdestroy.GameObjects.Add(this);
                    myLevel.GameObjects.Remove(this);
                }
            }
        }
    }
}
