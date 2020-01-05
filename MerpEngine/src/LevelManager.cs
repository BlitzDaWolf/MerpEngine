using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MerpEngine
{
    public static class LevelManager
    {
        public static List<Level> Levels = new List<Level>();

        public static void loadLevels()
        {
            if (Directory.Exists("levels"))
            {
                string[] lvl = Directory.GetFiles("levels", "*.lvl");
                for (int i = 0; i < lvl.Length; i++)
                {
                    Levels.Add(ContentPipe.LoadLevel(lvl[i]));
                }
            }
        }
    }
}
