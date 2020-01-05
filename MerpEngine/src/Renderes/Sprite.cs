﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngine.Renderes
{
    public class Sprite
    {
        public int sizePerPixel = 64;
        public Vector2 Position;

        public int RenderOrder = 0;

        public Material Material;

        public Vector2 Size
        {
            get => new Vector2(sizePerPixel / Material.texture.Size.X, sizePerPixel / Material.texture.Size.Y);
        }

        public virtual void Render() => SpriteBatch.Draw(Material.texture, Position * sizePerPixel, Size, Vector2.Zero);
    }
}