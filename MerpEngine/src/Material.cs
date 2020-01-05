using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MerpEngine
{
    [Serializable]
    public class Material
    {
        internal static Dictionary<string, Material> Materials = new Dictionary<string, Material>();

        public string name;

        [NonSerialized]
        public Texture2D texture;

        public Material(string name, Texture2D texture2D)
        {
            if (!Materials.ContainsKey(name))
            {
                this.name = name;
                this.texture = texture2D;

                Materials.Add(name, this);
            }
        }
    }
}
