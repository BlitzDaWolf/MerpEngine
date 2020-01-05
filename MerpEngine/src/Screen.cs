using OpenTK;

namespace MerpEngine
{
    public class Screen
    {
        public static int Width { get; internal set; } = 800;
        public static int Heigth { get; internal set; } = 600;
        public static WindowBorder Border { get; internal set; } = WindowBorder.Resizable;
    }
}
