using MerpEngine.Compoments;
using MerpEngine.Renderes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MerpEngine
{
    [Serializable]
    public class Level
    {
        public string pathName = "";
        public string Name = "";

        public List<Compoment> compoments = new List<Compoment>();
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
            spriteRenderes.ForEach(x => x.Render());
        }
        internal void Update()
        {
            GameObjects.ForEach(i => i.Update());
        }

        public GameObject GetGameObject(string name) => GameObjects.FirstOrDefault(x => x.Name == name);
        public GameObject[] GetGameObjects(string name) => GameObjects.Where(x => x.Name == name).ToArray();

        public GameObject[] GetGameObjectsWithType<T>() where T : Compoment
        {
            List<GameObject> objects = new List<GameObject>();
            foreach (var item in GameObjects)
            {
                if(item.GetType() == typeof(T))
                {
                    objects.Add(item);
                }
            }
            return objects.ToArray();
        }

        public string Save() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
        internal void Destroy() => compoments.ForEach(x => x.Destroy());
        internal void Start()
        {
            Camera.Main.SetPosition(Vector2.Zero);
            GameObjects.ForEach(x => x.Start());
        }
    }
}
