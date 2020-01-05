using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Input;

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
        }

        public static bool KeyPress(Key key) => (keysDown.Contains(key) && !keysDownLast.Contains(key));
        public static bool KeyRelease(Key key) => (!keysDown.Contains(key) && keysDownLast.Contains(key));
        public static bool KeyDown(Key key) => (keysDown.Contains(key));

        public static bool MouseButtonPress(MouseButton buton) => (buttonsDown.Contains(buton) && !buttonsDownLast.Contains(buton));
        public static bool MouseButtonRelease(MouseButton buton) => (!buttonsDown.Contains(buton) && buttonsDownLast.Contains(buton));
        public static bool MouseButtonDown(MouseButton buton) => (buttonsDown.Contains(buton));
    }
}
