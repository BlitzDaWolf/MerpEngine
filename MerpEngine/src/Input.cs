using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;

namespace MerpEngine
{
    public class Input
    {
        private static List<Key> keysDown;
        private static List<Key> keysDownLast;
        private static List<MouseButton> buttonsDown;
        private static List<MouseButton> buttonsDownLast;

        public static Vector2 MousePosition { get; private set; }

        public static void Initialize(GameWindow game)
        {
            keysDown = new List<Key>();
            keysDownLast = new List<Key>();

            buttonsDown = new List<MouseButton>();
            buttonsDownLast = new List<MouseButton>();

            game.KeyDown += Game_KeyDown;
            game.KeyUp += Game_KeyUp;

            game.MouseUp += Game_MouseUp;
            game.MouseDown += Game_MouseDown;

            game.MouseMove += Game_MouseMove;
            game.MouseWheel += Game_MouseWheel;
        }

        private static int mouseScroll = 0;
        public static float ScrollWeigth = 5;

        private static float mouseScrollWheel = 0;
        public static float MouseScrollWheel => mouseScrollWheel * ScrollWeigth;

        private static void Game_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int diference = mouseScroll - e.Mouse.ScrollWheelValue;
            mouseScroll = e.Mouse.ScrollWheelValue;
            mouseScrollWheel = diference;
        }

        private static void Game_MouseMove(object sender, MouseMoveEventArgs e)
        {
            MousePosition = new Vector2(e.X, e.Y);
        }

        private static void Game_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonsDown.Add(e.Button);
        }
        private static void Game_MouseUp(object sender, MouseButtonEventArgs e)
        {
            while (buttonsDown.Contains(e.Button))
            {
                buttonsDown.Remove(e.Button);
            }
        }
        private static void Game_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            while (keysDown.Contains(e.Key))
            {
                keysDown.Remove(e.Key);
            }
        }
        private static void Game_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keysDown.Add(e.Key);
        }

        public static void Update()
        {
            keysDownLast = new List<Key>(keysDown);
            buttonsDownLast = new List<MouseButton>(buttonsDown);

            mouseScrollWheel = (float)Math.Round(Vector2.Lerp(new Vector2(mouseScrollWheel, 0), Vector2.Zero, 0.5f).X, 5);
            if (Math.Abs(mouseScrollWheel) < 0.0005)
            {
                mouseScrollWheel = 0;
            }
        }

        float lerp(int a, int b, float time)
        {
            return b - (a * time);
        }

        public static bool KeyPress(Key key) => (keysDown.Contains(key) && !keysDownLast.Contains(key));
        public static bool KeyRelease(Key key) => (!keysDown.Contains(key) && keysDownLast.Contains(key));
        public static bool KeyDown(Key key) => (keysDown.Contains(key));

        public static bool MouseButtonPress(MouseButton buton) => (buttonsDown.Contains(buton) && !buttonsDownLast.Contains(buton));
        public static bool MouseButtonRelease(MouseButton buton) => (!buttonsDown.Contains(buton) && buttonsDownLast.Contains(buton));
        public static bool MouseButtonDown(MouseButton buton) => (buttonsDown.Contains(buton));
    }
}
