using MerpEngine.Compoments;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace MerpEngine.GUI
{
    public class EventHandeler : Compoment
    {
        public override void Start()
        {
            base.Start();
            base.GameObject.Name = "EventHandeler";
            GameObject.DontDestroyOnLoad();
        }

        public override void Update() { }

        public bool isHovering(UI ui)
        {
            Vector2 mousePosition = Input.MousePosition;
            Vector2 offset = Vector2.Zero;

            Vector2 topLeft = new Vector2(ui.Rect.X, ui.Rect.Y);
            Vector2 bottomRight = new Vector2(ui.Rect.X + ui.Rect.Right, ui.Rect.Y + ui.Rect.Bottom);

            return (mousePosition.X < topLeft.X 
                    && mousePosition.Y < topLeft.Y)
                && (mousePosition.X > bottomRight.X 
                    && mousePosition.Y > bottomRight.Y);
        }

        public T GetUI<T>(string name) where T : UI
        {
            GameObject go = LevelManager.LoadedLevel.GetGameObject(name);
            if(go != null) return go.GetCompoment<T>();
            return null;
        }

        public T[] GetUIS<T>(string name) where T : UI => LevelManager.LoadedLevel.GetGameObjects(name).Where(x => x.GetCompoment<T>() != null).Select(x => x.GetCompoment<T>()).ToArray();

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UI));
            TextWriter writer = new StringWriter();
            serializer.Serialize(writer, null);
        }

        public void Load(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("data/UI/UI1.xml");

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "Button")
                {
                    new Button(node);
                }
                else if (node.Name == "Text")
                {
                    new UIText(node);
                }
                else
                {
                    new UI(node);
                }
            }
        }
    }
}
