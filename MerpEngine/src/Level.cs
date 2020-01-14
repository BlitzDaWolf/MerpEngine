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
            GameObjects.ForEach(i => i.Render());
        }
        internal void Update()
        {
            GameObjects.ForEach(i => i.Update());
        }

        public Compoment GetCompoment(string name) => compoments.FirstOrDefault(x => x.Name == name);

        public string Save() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
        internal void Destroy() => compoments.ForEach(x => x.Destroy());
        internal void Start()
        {
            Camera.Main.SetPosition(Vector2.Zero);
            GameObjects.ForEach(x => x.Start());
        }
    }
}
