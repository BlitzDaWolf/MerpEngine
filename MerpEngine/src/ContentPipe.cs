using OpenTK.Graphics.OpenGL;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MerpEngine
{
    public static class ContentPipe
    {
        private static Dictionary<string, Tuple<string, Action<Material>>> toLoadMaterial = new Dictionary<string, Tuple<string, Action<Material>>>();

        internal static void loadMaterials()
        {
            foreach (string item in toLoadMaterial.Keys)
            {
                var tex = LoadTexture(toLoadMaterial[item].Item1);
                Material m =  new Material(item, tex);
                toLoadMaterial[item].Item2(m);
            }
            toLoadMaterial = new Dictionary<string, Tuple<string, Action<Material>>>();
        }

        public static void LoadMaterial(string name, string path, Action<Material> callback)
        {
            if (Material.Materials.ContainsKey(name))
            {
                callback(Material.Materials[name]);
            }
            else
            {
                if (toLoadMaterial.ContainsKey(name))
                {
                    Action<Material> newActions=new Action<Material>(callback);
                    newActions += toLoadMaterial[name].Item2;
                    toLoadMaterial[name] = new Tuple<string, Action<Material>>(toLoadMaterial[name].Item1, newActions);

                    // toLoadMaterial[name].Item2 += callback;
                }
                else
                {
                    toLoadMaterial.Add(name, new Tuple<string, Action<Material>>(path, callback));
                }
            }
        }

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

        public static void SaveLevel(Level level, string path)
        {
            BinaryFormatter formmater = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            formmater.Serialize(stream, level);
            stream.Close();
        }

        public static Level LoadLevel(string path)
        {
            if (File.Exists(path))
            {
                BinaryFormatter formmater = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                Level lvl = formmater.Deserialize(stream) as Level;
                stream.Close();
                return lvl;
            }
            else
            {
                return null;
            }
        }

        private static MemoryStream seriliazeObject(Level obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formmater = new BinaryFormatter();
            formmater.Serialize(stream, obj);
            return stream;
        }

        private static object DeserializeFromStream(MemoryStream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            object objectType = formatter.Deserialize(stream);
            return objectType;
        }

        public static Level GetLevelCopy(Level level)
        {
            Level l = new Level();
            l.compoments = level.compoments.ToList();
            l.Name = level.Name;
            l.GameObjects = level.GameObjects;
            return l;

            MemoryStream stream = seriliazeObject(level);
            return (Level)DeserializeFromStream(stream);
        }
    }
}
