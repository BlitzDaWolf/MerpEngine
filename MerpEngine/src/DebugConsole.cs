using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace MerpEngine
{
    public class DebugConsole
    {
        private static Dictionary<string, Action<string[]>> otherCommands = new Dictionary<string, Action<string[]>>();

        public static void AddCommand(string name, Action<string[]> action)
        {
            otherCommands.Add(name.ToLower(), action);
        }

        public bool ConsoleRunning = true;

        public DebugConsole()
        {
            Thread thr = new Thread(ConsoleleLoop);
            thr.Start();
        }

        public void ConsoleleLoop()
        {
            while (ConsoleRunning && Application.Running)
            {
                string rawCommand = Console.ReadLine();
                string[] command = rawCommand.Split(new char[] { ' ' });
                string baseCmd = command[0].ToLower();
                if(baseCmd == "set")
                {
                    Arguments.SetEnviroment("");
                }
                else if(baseCmd == "levels")
                {
                    Debug.Log(LevelManager.Levels.Count);
                }
                else if(baseCmd == "exit")
                {
                    Application.Quit();
                }
                else if(baseCmd == "jump")
                {
                    if (command.Length >= 3)
                    {
                        int x, y = 0;
                        if (int.TryParse(command[1], out x) && int.TryParse(command[2], out y))
                        {
                            Camera.Main.SetPosition(new OpenTK.Vector2(x, y));
                        }
                    }
                }
                else if(baseCmd == "materials")
                {
                    Debug.Log(Material.Materials.Count);
                }
                else if(baseCmd == "load")
                {
                    if(command.Length >= 2)
                    {
                        int levelId = -1;
                        if(int.TryParse(command[1], out levelId))
                        {
                            LevelManager.LoadLevel(levelId);
                        }
                    }
                }
                else if(baseCmd == "size")
                {
                    Debug.Log($"Screen size: {new Vector2(Screen.Width, Screen.Heigth)}");
                }
                else if (otherCommands.ContainsKey(baseCmd))
                {
                    otherCommands[baseCmd](command.Skip(1).ToArray());
                }
            }
        }
    }
}
