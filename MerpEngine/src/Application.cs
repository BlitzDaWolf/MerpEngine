using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngine
{
    public class Application
    {
        public static bool Running { get; internal set; }
        internal static void Start()
        {
            Running = true;
        }
        public static void Quit()
        {
            Running = false;
        }
    }
}
