using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Threading;
using Color = OpenTK.Color;

namespace MerpEngine
{
    public class Game
    {
        public GameWindow window;
        Camera view;

        Level level;

        public static void Start()
        {
            new Camera(Vector2.Zero);
            Debug.Info($"Loading levels");
            LevelManager.loadLevels();
            Debug.Info($"loaded {LevelManager.Levels.Count} level's");

            Thread t = new Thread(() =>
            {
                Debug.Info("Loading inputs");
                ContentPipe.LoadInput();

                Debug.Info($"Loaded: {AxiesManager.instance.Axies.Count} Axies");
                Debug.Info($"Loaded: {AxiesManager.instance.Axies2D.Count} 2D axies");

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

            view = Camera.Main;
            Screen.Width = window.Width;
            Screen.Heigth = window.Height;

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

            view.Update();

            Input.Update();
            ContentPipe.loadMaterials();
            LevelManager._LaodedLevel.Update();

            if (sw.ElapsedMilliseconds > (1 * 1000))
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
            LevelManager._LaodedLevel.Render();
            #endregion

            frames++;

            window.SwapBuffers();
        }
    }
}
