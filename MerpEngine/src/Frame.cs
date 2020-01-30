using System.Collections.Generic;
using System.Linq;

namespace MerpEngine
{
    public static class Frame
    {
        public static int CurrentFrame { get; internal set; }
        internal static Queue<int> frames = new Queue<int>();

        internal static void AddFrame(int a)
        {
            if (frames.Count > 60)
            {
                frames.Dequeue();
            }
            frames.Enqueue(a);
            FPS = a;
        }

        public static float avg => frames.Count > 0 ? (float)frames.Average() : 0;
        public static float FPS;
    }
}
