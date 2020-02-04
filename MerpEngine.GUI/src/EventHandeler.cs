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

        List<UI> HoverdUI = new List<UI>();

        public override void Update()
        {
            CheckHover();
            CheckIsHover();
        }

        private void CheckIsHover()
        {
            var uis = LevelManager.LoadedLevel.GetTypes<UI>().ToList();
            foreach (var item in uis)
            {
                if (isHovering(item))
                {
                    if (Input.MouseButtonPress(0))
                    {
                        item.OnClick();
                    }
                    if (!HoverdUI.Contains(item))
                    {
                        HoverdUI.Add(item);
                        item.OnHoverEnter();
                    }
                }
            }
        }
        private void CheckHover()
        {
            List<UI> copy = new List<UI>(HoverdUI);
            foreach (var item in copy)
            {
                if (!isHovering(item))
                {
                    HoverdUI.Remove(item);
                    item.OnHoverLeave();
                }
            }
        }

        public bool isHovering(UI ui)
        {
            Vector2 mousePosition = Camera.Main.ToWorld(Input.MousePosition);
            Vector2 offset = Vector2.Zero;

            Vector2 gP = ui.toWorldPosition();

            Vector2 topLeft = new Vector2(gP.X, gP.Y);
            Vector2 bottomRight = new Vector2(topLeft.X + ui.Rect.Right, topLeft.Y + ui.Rect.Bottom);

            return (mousePosition.X >= topLeft.X 
                    && mousePosition.Y >= topLeft.Y)
                && (mousePosition.X <= bottomRight.X 
                    && mousePosition.Y <= bottomRight.Y);
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

            Canvas canvas = new GameObject().AddCompoment<Canvas>();
            canvas.readXml(doc.DocumentElement);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                UI u;

                if (node.Name == "Button")
                {
                    u = new Button(node);
                }
                else if (node.Name == "Text")
                {
                    u = new UIText(node);
                }
                else
                {
                    u = new UI(node);
                }
                canvas.AddElement(u);
            }
        }
    }
}
