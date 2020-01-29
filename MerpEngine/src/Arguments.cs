using OpenTK;
using System;
using System.Collections.Generic;

namespace MerpEngine
{
    public static class Arguments
    {
        public static string[] SetEnviroments(string[] args)
        {
            Queue<string> arguments = new Queue<string>(args);

            List<string> newArguments = new List<string>();

            while (arguments.Count > 0)
            {
                string argument = arguments.Dequeue().ToLower();
                if (argument.Contains("--debug"))
                {
                    Debug.DisplayInfo = true;
                    Debug.DisplayLogs = true;
                }
                else if (argument.Contains("--width"))
                {
                    string width = getSecond(argument, ref arguments);
                    int o = 0;
                    if (int.TryParse(width, out o))
                    {
                        Screen.Width = o;
                    }
                    else
                    {
                        arguments = placeFront(width, arguments);
                    }
                }
                else if (argument.Contains("--heigth"))
                {
                    string heigth = getSecond(argument, ref arguments);
                    int o = 0;
                    if (int.TryParse(heigth, out o))
                    {
                        Screen.Heigth = o;
                    }
                    else
                    {
                        arguments = placeFront(heigth, arguments);
                    }
                }
                else if (argument.Contains("--borderless"))
                {
                    Screen.Border = WindowBorder.Hidden;
                }
                else
                {
                    newArguments.Add(argument);
                }
            }

            return newArguments.ToArray();
        }

        internal static void SetEnviroment(string argument)
        {

        }

        private static Queue<string> placeFront(string a, Queue<string> old)
        {
            Queue<string> s = new Queue<string>();
            s.Enqueue(a);
            while (old.Count > 0)
            {
                s.Enqueue(old.Dequeue());
            }
            return s;
        }

        private static string getSecond(string arg1, ref Queue<string> args)
        {
            if (arg1.Contains("="))
            {
                string[] d = arg1.Split(new char[] { '=' });
                return d[1];
            }
            else
            {
                return args.Dequeue();
            }
        }
    }
}
