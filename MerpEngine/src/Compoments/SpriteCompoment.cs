﻿using MerpEngine.Renderes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngine.Compoments
{
    [Serializable]
    public class SpriteCompoment : Compoment
    {
        public string meshName = "";
        public string pathName = "";

        [NonSerialized]
        public Sprite sprite;

        public void Render()
        {
            if (sprite == null) return;
            if (sprite.Material == null) return;
            SpriteBatch.Draw(sprite.Material.texture, (Position * sprite.sizePerPixel) + sprite.Position, Scale * sprite.Size, Vector2.Zero);
        }

        public override void Start()
        {
            LoadNewSprite(meshName, pathName);
        }

        #region Load sprite
        public void LoadNewSprite(Sprite sprite)
        {
            this.sprite = sprite;
        }
        public void LoadNewSprite(Material m)
        {
            LoadNewSprite(new Sprite() { Material = m });
        }
        public void LoadNewSprite(string name)
        {
            if (Material.Materials.ContainsKey(name))
            {
                LoadNewSprite(Material.Materials[name]);
            }
        }
        public void LoadNewSprite(string name, string path)
        {
            if (string.IsNullOrEmpty(meshName) && string.IsNullOrEmpty(pathName)) return;
            if (Material.Materials.ContainsKey(name))
            {
                LoadNewSprite(Material.Materials[name]);
            }
            else
            {
                ContentPipe.LoadMaterial(name, path, LoadNewSprite);
            }
        }
        #endregion
    }
}
