using System.Diagnostics;

namespace MerpEngine
{
    public static class Time
    {
        internal static float deltaTime;
        public static float DeltaTime { get => deltaTime; }
        internal static Stopwatch startTimer;

        public static float TimePasted {get; internal set;}
        public static long TimeTicks => startTimer.ElapsedMilliseconds;
        public static Stopwatch test => startTimer;
    }
}
