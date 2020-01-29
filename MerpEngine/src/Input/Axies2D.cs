using OpenTK;
using System;

namespace MerpEngine
{
    [Serializable]
    public class Axies2D
    {
        public string Horizontal;
        public string Vertical;

        public Vector2 GetAxies()
        {
            float x = AxiesManager.instance._GetAxis(Horizontal);
            float y = AxiesManager.instance._GetAxis(Vertical);

            return new Vector2(x, y);
        }
    }
}
