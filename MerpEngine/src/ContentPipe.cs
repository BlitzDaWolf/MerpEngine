﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MerpEngine
{
    public class ContentPipe
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">Defaults to "Content/"</param>
        /// <param name="pixelated"></param>
        /// <returns></returns>
        public static Texture2D LoadTexture(string filePath, bool pixelated = true)
        {
            filePath = $"Content/{filePath}";
            if (!File.Exists(filePath))
            {
                throw new Exception($"File does not exsist at '{filePath}'");
            }

            Bitmap bmp = new Bitmap(filePath);
            return LoadTexture(bmp, pixelated);
        }

        public static Texture2D LoadTexture(Bitmap bmp, bool pixelated = true)
        {
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, pixelated ? (int)TextureMinFilter.Nearest : (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, pixelated ? (int)TextureMagFilter.Nearest : (int)TextureMagFilter.Linear);

            return new Texture2D(id, new OpenTK.Vector2(bmp.Width, bmp.Height));
        }

        /// <summary>
        /// Loads a level from a file
        /// </summary>
        /// <param name="filePath">The path of the file</param>
        /// <returns>retuns a level</returns>
        public static Level LoadLevel(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"File does not exsists at '{filePath}'");
            }

            Level lvl = Newtonsoft.Json.JsonConvert.DeserializeObject<Level>(File.ReadAllText(filePath));

            return lvl;
        }
    }
}
