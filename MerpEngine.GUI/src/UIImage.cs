using System;
using System.Xml;
using MerpEngine.Renderes;

namespace MerpEngine.GUI
{
    public class UIImage : UI
    {
        public UIImage(){}
        public UIImage(XmlNode node) : base(node)
        {
            var defaultImage = node.Attributes.GetNamedItem("default");
            if(defaultImage != null){
                base.sprite = Sprite.LoadSprite(defaultImage.Value);
                Debug.Log(defaultImage.Value);
            }
        }
    }
}