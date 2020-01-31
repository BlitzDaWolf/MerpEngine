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
            if (otherCommands.ContainsKey(name))
            {
                otherCommands[name] += action;
            }
            else
            {
                otherCommands.Add(name.ToLower(), action);
            }
        }

        public bool ConsoleRunning = true;
        Thread thr;

        public DebugConsole()
        {
            thr = new Thread(ConsoleleLoop);
            thr.Start();
        }

        internal void Close(object sender, EventArgs e) { }

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
                else if(baseCmd == "fps")
                {
                    Debug.Log($"AVG {Frame.avg}");
                    Debug.Log($"last {Frame.FPS}");
                }
                else if(baseCmd == "clear")
                {
                    Console.Clear();
                }
                else if(baseCmd == "gameobjects")
                {
                    Debug.Log($"Loaded gameobjects: {LevelManager.LoadedLevel.GameObjects.Count + LevelManager.Dontdestroy.GameObjects.Count}");
                    Debug.Log($"Active gameobjects: {LevelManager.LoadedLevel.GameObjects.Where(x=>x.Active).Count() + LevelManager.Dontdestroy.GameObjects.Where(x => x.Active).Count()}");
                }
                else if(baseCmd == "exec")
                {
                    if(command.Length > 1)
                    {
                        if(command[1].ToLower() == "getgameobject")
                        {
                            Debug.Log(LevelManager.LoadedLevel.GetGameObject(command[2]));
                        }
                    }
                }
                else if (otherCommands.ContainsKey(baseCmd))
                {
                    otherCommands[baseCmd](command.Skip(1).ToArray());
                }
            }
        }
    }
}
