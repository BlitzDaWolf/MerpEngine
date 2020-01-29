using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngine
{
    public static class Vector2Extension
    {
        public static float Distance(Vector2 a, Vector2 b)
        {
            double x = (Math.Abs(a.X - b.X));
            double y = (Math.Abs(a.Y - b.Y));

            x = x * x;
            y = y * y;

            double res = Math.Sqrt(x + y);

            return (float)res;
        }
    }
}
