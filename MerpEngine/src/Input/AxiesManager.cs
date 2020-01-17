using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngine
{
    [Serializable]
    public class AxiesManager
    {
        public static AxiesManager instance;

        public Dictionary<string, Axies> Axies = new Dictionary<string, Axies>();
        public Dictionary<string, Axies2D> Axies2D = new Dictionary<string, Axies2D>();

        public AxiesManager()
        {
            instance = this;
        }

        internal float _GetAxis(string name)
        {
            if (Axies.ContainsKey(name))
            {
                return Axies[name].GetAxies();
            }
            return 0f;
        }
        internal Vector2 _Get2DAxies(string name)
        {
            if (Axies2D.ContainsKey(name))
            {
                return Axies2D[name].GetAxies();
            }
            return Vector2.Zero;
        }

        public static float GetAxis(string name) => instance._GetAxis(name);
        public static Vector2 Get2DAxies(string name) => instance._Get2DAxies(name);

        public static Axies GetAxiesRaw(string name)
        {
            if (instance.Axies.ContainsKey(name))
            {
                return instance.Axies[name];
            }
            return null;
        }
    }
}