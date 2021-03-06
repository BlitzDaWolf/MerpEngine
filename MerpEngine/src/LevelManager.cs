﻿using System.Collections.Generic;
using System.IO;

namespace MerpEngine
{
    public static class LevelManager
    {
        public static List<Level> Levels = new List<Level>();
        public static Level LoadedLevel => _LaodedLevel;
        internal static Level _LaodedLevel;

        internal static bool startLevel = false;

        public static Level Dontdestroy { get; private set; }

        public static int loadedLevel { get; private set; } = 0;

        internal static void loadLevels()
        {

            Dontdestroy = new Level();
            Dontdestroy.DontDestroyOnLoad = true;
            Dontdestroy.Name = "Don't destroy on load";

            if (Directory.Exists("data/levels"))
            {
                string[] lvl = Directory.GetFiles("data/levels", $"*{ContentPipe.LEVEL_EXSTENTION}");
                for (int i = 0; i < lvl.Length; i++)
                {
                    Levels.Add(ContentPipe.LoadLevel(lvl[i]));
                }
            }
            if (Levels.Count > 0)
            {
                loadedLevel = 0;
                SetLevel();
            }
            else
            {
                Debug.Error("No levels where found shutting down");
                // Shut down game
                Application.Quit();
            }
        }

        private static void SetLevel()
        {
            startLevel = false;
            _LaodedLevel = ContentPipe.GetLevelCopy(Levels[loadedLevel]);
            startLevel = true;
            _LaodedLevel.Start();
        }


        public static void LoadLevel(int number)
        {
            if(number < Levels.Count && number >= 0){
                if (number != loadedLevel)
                {
                    _LaodedLevel.Destroy();
                    loadedLevel = number;
                    SetLevel();
                }
            }
            else
            {
                Debug.Error($"Level {number} out of range");
            }
        }
    }
}
