using System;
using System.Collections.Generic;

namespace MerpEngine
{
    [Serializable]
    public class Material
    {
        internal static Dictionary<string, Material> Materials = new Dictionary<string, Material>();

        public string name;
        public string path;

        [NonSerialized]
        public Texture2D texture;

        public Material(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public Material(string name, string path, Texture2D texture2D)
        {
            if (!Materials.ContainsKey(name))
            {
                this.name = name;
                this.path = path;
                this.texture = texture2D;

                Materials.Add(name, this);
            }
        }

        public Material(Texture2D texture)
        {
            this.texture = texture;
        }

        internal void Reload()
        {
            if (Materials.ContainsKey(name))
            {
                texture = Materials[name].texture;
            }
            else
            {
                ContentPipe.LoadMaterial(name, path, (mat) => texture = mat.texture);
            }
        }
    }
}
