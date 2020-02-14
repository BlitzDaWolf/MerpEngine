using MerpEngine.Renderes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MerpEngine.GUI
{
    [Serializable]
    public class Button : UI
    {
        public Sprite Default;
        public Sprite OnHover;
        public Sprite OnClick;

        public Button()
        {
            
        }

        #region ReadXML
        public Button(XmlNode node) : base(node)
        {
            var atr = node.Attributes;
            if(atr != null){
                var def = node.Attributes.GetNamedItem("default");
                var hover = node.Attributes.GetNamedItem("hover");
                var click = node.Attributes.GetNamedItem("click");

                if(def != null) LoadDefualt(def.Value);
                if(hover != null) LoadOnHover(hover.Value);
                if(click != null) LoadOnClick(click.Value);
            }

            HoverEnter += hovering;
            HoverLeave += hoverLeave;
            base.sprite = Default;
        }

        private void LoadDefualt(string value) => Default = Sprite.LoadSprite(value);
        private void LoadOnHover(string value) => OnHover = Sprite.LoadSprite(value);
        private void LoadOnClick(string value) => OnClick = Sprite.LoadSprite(value);
        #endregion

        public override void Start()
        {
            base.sprite = Default;
        }

        private void hovering(object sender, EventArgs args){
            base.sprite = OnHover;
        }
        private void hoverLeave(object sender, EventArgs args){
            base.sprite = Default;
        }

        public override void Update()
        {
            
        }
    }
}
