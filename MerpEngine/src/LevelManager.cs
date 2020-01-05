using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MerpEngine
{
    public static class LevelManager
    {
        public static List<Level> Levels = new List<Level>();
        internal static Level LaodedLevel;
        public static int loadedLevel { get; private set; } = 0;

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
            if(Levels.Count > 0)
            {
                loadedLevel = 0;
                SetLevel();
            }
            else
            {
                Debug.Error("No levels where found shutting down");
                // Shut down game
            }
        }

        private static void SetLevel()
        {
            LaodedLevel = Newtonsoft.Json.JsonConvert.DeserializeObject<Level>(Levels[loadedLevel].Save());
        }

        public static void Peek()
        {
            Debug.Log(Levels[loadedLevel].Save());
        }

        public static void LoadLevel(int number)
        {
            if(number != loadedLevel)
            {
                LaodedLevel.Destroy();
                loadedLevel = number;
                SetLevel();
                LaodedLevel.Start();
            }
        }
    }
}
