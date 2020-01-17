using MerpEngine;
using MerpEngine.Compoments;
using MerpEngineExample.Compoments;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MerpEngineExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ContentPipe.LoadInput();
            AxiesManager.instance.Axies2D = new System.Collections.Generic.Dictionary<string, Axies2D>();
            AxiesManager.instance.Axies = new System.Collections.Generic.Dictionary<string, Axies>();
            
            AxiesManager.instance.Axies2D.Add("Movement", new Axies2D() { Horizontal = "Horizontal", Vertical = "Vertical" });

            AxiesManager.instance.Axies.Add("Horizontal", new Axies() { PositiveKey = OpenTK.Input.Key.D, NegitiveKey = OpenTK.Input.Key.A });
            AxiesManager.instance.Axies.Add("Vertical", new Axies() { PositiveKey = OpenTK.Input.Key.W, NegitiveKey = OpenTK.Input.Key.S, Inverted = true });
            AxiesManager.instance.Axies.Add("Escape", new Axies() { PositiveKey = OpenTK.Input.Key.Escape });
            ContentPipe.SaveInput();

            Arguments.SetEnviroments(args);

            Game.Start();
        }
    }
}
