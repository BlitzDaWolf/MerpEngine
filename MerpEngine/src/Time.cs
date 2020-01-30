using System.Diagnostics;

namespace MerpEngine
{
    public static class Time
    {
        internal static float deltaTime;
        public static float DeltaTime { get => deltaTime; }
        internal static Stopwatch startTimer;

        public static float TimePasted => startTimer.ElapsedMilliseconds / 100f;
    }
}
