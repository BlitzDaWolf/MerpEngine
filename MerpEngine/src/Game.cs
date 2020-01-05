using MerpEngine.GUI;
using MerpEngine.Renderes;
using MerpEngine;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Diagnostics;
using System.Drawing;
using Color = OpenTK.Color;
using System.Threading;

namespace MerpEngine
{
    public class Game
    {
        public GameWindow window;
        EventHandeler handeler;
        View view;

        Level level;

        public static void Start()
        {

            Thread t = new Thread(() => {
                Debug.Info("Starting game client");
                Debug.Info($"Starting game with [{Screen.Width}, {Screen.Heigth}]");

                GameWindow window = new GameWindow(Screen.Width, Screen.Heigth);
                window.WindowBorder = Screen.Border;
                window.X = 0;
                window.Y = 0;

                Game game = new Game(window);
                window.Run();
            });
            t.Start();
        }

        public Game(GameWindow windowInput)
        {
            this.window = windowInput;

            window.Load += Window_Load;
            window.RenderFrame += Window_RenderFrame;
            window.UpdateFrame += Window_UpdateFrame;
            window.Closing += Window_Closing;
            window.Resize += Window_Resize;

            Screen.Width = window.Width;
            Screen.Heigth = window.Height;

            view = new View(Vector2.Zero);
            Input.Initialize(window);
        }

        private void Window_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);

            Screen.Width = window.Width;
            Screen.Heigth = window.Height;
        }

        private void Window_Load(object sender, EventArgs e)
        {
            handeler = new EventHandeler();

            level = new Level();

            GL.ClearColor(Color.Blue);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        Stopwatch sw = Stopwatch.StartNew();
        int frames = 0;

        int frameCount = 0;

        private void Window_UpdateFrame(object sender, FrameEventArgs e)
        {
            Time.deltaTime = (float)e.Time;

            handeler.Update();
            view.Update();
            Input.Update();
            level.Update();

            if(sw.ElapsedMilliseconds > (1 * 1000))
            {
                frameCount = frames;
                frames = 0;
                Frame.AddFrame(frameCount);
                sw.Restart();
            }

            window.Title = $"{Frame.avg}";
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            SpriteBatch.Begin(window.Width, window.Height);

            view.ApplyTransform();


            #region Sprites
            level.Render();
            #endregion

            #region GUI
            handeler.Render();
            #endregion

            frames++;

            window.SwapBuffers();
        }
    }
}
