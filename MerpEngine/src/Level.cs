using MerpEngine.Compoments;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MerpEngine
{
    [Serializable]
    public class Level
    {
        public  bool DontDestroyOnLoad { get; internal set; }
        public string pathName = "";
        public string Name = "";

        public List<GameObject> GameObjects { get; set; } = new List<GameObject>();

        internal void Render()
        {
            List<SpriteCompoment> spriteRenderes = new List<SpriteCompoment>();

            GetGameObjectsWithType<SpriteCompoment>()
                .ToList()
                .ForEach(x => spriteRenderes
                    .Add(
                        x.GetCompoment<SpriteCompoment>()));

            spriteRenderes = spriteRenderes.OrderBy(x => x.RenderIndex).ToList();
            spriteRenderes.ForEach(x => {
                if (x.GameObject.Active)
                    x.Render();
            });
        }
        internal void Update()
        {
            List<GameObject> GameObjectsCoppy = new List<GameObject>(GameObjects);
            GameObjectsCoppy.ForEach(i =>
            {
                if (i.Active)
                    i.Update();
            });
        }

        public GameObject GetGameObject(string name)
        {
            if (!DontDestroyOnLoad)
            {
                if (LevelManager.Dontdestroy.GetGameObject(name) != null)
                {
                    return LevelManager.Dontdestroy.GetGameObject(name);
                }
            }
            return GameObjects.FirstOrDefault(x => x.Name == name);
        }
        public GameObject[] GetGameObjects(string name)
        {
            List<GameObject> objects = new List<GameObject>();
            if (this != LevelManager.Dontdestroy)
            {
                objects.AddRange(LevelManager.Dontdestroy.GetGameObjects(name));
            }
            objects.AddRange(GameObjects.Where(x => x.Name == name));
            return objects.ToArray();
        }

        public GameObject[] GetGameObjectsWithType<T>() where T : Compoment
        {
            List<GameObject> objects = new List<GameObject>();
            if (this != LevelManager.Dontdestroy)
            {
                objects.AddRange(LevelManager.Dontdestroy.GetGameObjectsWithType<T>());
            }
            foreach (var item in GameObjects)
            {
                if (item.HasCompoment<T>())
                {
                    objects.Add(item);
                }
            }
            return objects.ToArray();
        }
        
        public T[] GetTypes<T>() where T : Compoment
        {
            var gameObjects = GetGameObjectsWithType<T>().ToList();
            List<T> comps = new List<T>();
            gameObjects.ForEach(x =>
            {
                comps.AddRange(x.GetCompoments<T>());
            });
            return comps.ToArray();
        }

        internal void Destroy() { }
        internal void Start()
        {
            if (!LevelManager.startLevel) return;

            Debug.Info("Starting level");
            Debug.Info($"{GameObjects.Count} gameobjects laoded");

            List<GameObject> coppyGameobjects = new List<GameObject>(GameObjects);

            List<SpriteCompoment> spriteRenderes = new List<SpriteCompoment>();

            GetGameObjectsWithType<SpriteCompoment>()
                .ToList()
                .ForEach(x => spriteRenderes
                    .Add(
                        x.GetCompoment<SpriteCompoment>()));

            spriteRenderes.ForEach(x =>
            {
                if (x.sprite == null) return;
                x.sprite.Material.Reload();
            });

            Camera.Main.SetPosition(Vector2.Zero);
            coppyGameobjects.ForEach(x => x.Start());
        }
    }
}
