﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MerpEngine
{
    public static class ContentPipe
    {
        public const string LEVEL_EXSTENTION = ".lvl";
        public const string INPUT_EXSTENTION = ".json";

        private static Dictionary<string, Tuple<string, Action<Material>>> toLoadMaterial = new Dictionary<string, Tuple<string, Action<Material>>>();

        internal static void loadMaterials()
        {
            foreach (string item in toLoadMaterial.Keys)
            {
                var tex = LoadTexture(toLoadMaterial[item].Item1);
                Material m = new Material(item, toLoadMaterial[item].Item1, tex);
                toLoadMaterial[item].Item2(m);
            }
            toLoadMaterial = new Dictionary<string, Tuple<string, Action<Material>>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">Defaults to "Content/"</param>
        /// <param name="pixelated"></param>
        /// <returns></returns>
        public static Texture2D LoadTexture(string filePath, bool pixelated = true)
        {
            filePath = $"data/Content/{filePath}";
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

        public static void DisposeTexture(int id)
        {
            GL.DeleteTexture(id);
        }

        public static void SaveLevel(Level level, string path)
        {
            BinaryFormatter formmater = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            formmater.Serialize(stream, level);
            stream.Close();
        }

        public static void SaveJsonLevel(Level level, string path)
        {
            Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(level));
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

        public static Level GetLevelCopy(Level level) => (Level)DeserializeFromStream(seriliazeObject(level));

        public static void SaveInput()
        {
            File.WriteAllText("data/settings/Input" + INPUT_EXSTENTION, Newtonsoft.Json.JsonConvert.SerializeObject(AxiesManager.instance, Newtonsoft.Json.Formatting.Indented));
        }

        public static void LoadInput()
        {
            try
            {
                AxiesManager.instance = Newtonsoft.Json.JsonConvert.DeserializeObject<AxiesManager>(File.ReadAllText("data/settings/Input" + INPUT_EXSTENTION));
            }
            catch (Exception ex)
            {
                AxiesManager ax = new AxiesManager();
                Debug.Error(ex);
            }
        }

        public static void LoadMaterials()
        {
            if(Directory.Exists("data/material"))
            {
                var mats = Directory.GetFiles("data/material", "*.json");
                for(int i = 0; i < mats.Length; i++){
                    Debug.Log($"Loading {mats[i]}");
                    LoadMaterials(File.ReadAllText(mats[i]));
                }
            }
        }

        public static void LoadMaterials(string value)
        {
            List<Material> ms = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Material>>(value);
            ms.ForEach(x => x.Reload());
        }
    }
}
