using OpenTK;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

namespace MerpEngine.GUI
{
    public class Canvas : Compoment
    {
        public Vector2 Position => _position;
        public bool FixedToCamera;
        public Vector2 Size { get; set; }

        private Vector2 _position;

        public List<UI> UIElements = new List<UI>();

        public void SetPosition(Vector2 newPosition)
        {
            if (!FixedToCamera)
            {
                _position = newPosition;
            }
        }

        public override void Update()
        {
            if (FixedToCamera)
            {
                _position = Camera.Main.Position;
                Size = new Vector2(Screen.Width, Screen.Heigth);
            }
        }

        public void AddElement(UI elm)
        {
            UIElements.Add(elm);
            elm.Canvas = this;
        }
        public UI GetElementByName(string name) => UIElements.Where(x => x.Name == name).FirstOrDefault();
        public T GetElementByName<T>(string name) where T : UI => UIElements.Where(x => x is T && x.Name == name).FirstOrDefault() as T;

        internal void readXml(XmlElement documentElement)
        {
            var fixedCamera = documentElement.GetAttribute("fixed");
            if(!string.IsNullOrEmpty(fixedCamera))
            {
                bool.TryParse(fixedCamera, out FixedToCamera);
            }
            else
            {
                var size = documentElement.GetAttribute("size");
                var position = documentElement.GetAttribute("position");

                if (!string.IsNullOrEmpty(size))
                {
                    string[] splited = size.Split(new char[] { ',' });
                    if (splited.Length >= 2)
                    {
                        int x, y;
                        int.TryParse(splited[0], out x);
                        int.TryParse(splited[1], out y);

                        Size = new Vector2(x, y);
                    }
                }
                if (!string.IsNullOrEmpty(position))
                {
                    string[] splited = position.Split(new char[] { ',' });
                    if (splited.Length >= 2)
                    {
                        int x, y;
                        int.TryParse(splited[0], out x);
                        int.TryParse(splited[1], out y);

                        _position = new Vector2(x, y);
                    }
                }
            }
        }
    }
}
