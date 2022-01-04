using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tartarus
{
    /// <summary>
    /// Maintains input for the entire game. Covers mouse and keybouard input.
    /// </summary>
    static class Input
    {
        private static KeyboardState currKeyboardState;
        private static KeyboardState prevKeyboardState;
        private static MouseState currMouseState;
        private static MouseState prevMouseState;

        public static Vector2 MousePos => (currMouseState.Position.ToVector2() / TartarusGame.Instance.Scene.Camera.Scale) + TartarusGame.Instance.Scene.Camera.Position;

        public static void Initialize()
        {
            currKeyboardState = Keyboard.GetState();
            prevKeyboardState = Keyboard.GetState();
            currMouseState = Mouse.GetState();
            prevMouseState = Mouse.GetState();
        }

        public static void Update()
        {
            prevKeyboardState = currKeyboardState;
            currKeyboardState = Keyboard.GetState();
            prevMouseState = currMouseState;
            currMouseState = Mouse.GetState();
        }

        public static bool Check(Keys key)
        {
            return currKeyboardState.IsKeyDown(key);
        }

        public static bool CheckDelayed(Keys key)
        {
            throw new Exception();

        }
        
        public static bool Pressed(Keys key)
        {
            return currKeyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key);
        }

        public static bool Released(Keys key)
        {
            return currKeyboardState.IsKeyUp(key) && prevKeyboardState.IsKeyDown(key);
        }

        public static bool LeftClick()
        {
            return currMouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool LeftMouseDown()
        {
            return currMouseState.LeftButton == ButtonState.Pressed;
        }

        #region MAPPING

        private static Keys up = Keys.Up;
        private static Keys down = Keys.Down;
        private static Keys left = Keys.Left;
        private static Keys right = Keys.Right;
        private static Keys a = Keys.Space;
        private static Keys b = Keys.C;
        private static Keys start = Keys.T;

        private static Keys GetMapping(MappedKeys mapkey)
        {
            switch (mapkey)
            {
                case MappedKeys.Up:
                    return up;
                case MappedKeys.Down:
                    return down;
                case MappedKeys.Left:
                    return left;
                case MappedKeys.Right:
                    return right;
                case MappedKeys.A:
                    return a;
                case MappedKeys.B:
                    return b;
                case MappedKeys.Start:
                    return start;
                default:
                    throw new Exception("Mapped key not recongized.");
            }
        }

        public static void RemapKey(MappedKeys mapkey, Keys key)
        {
            switch (mapkey)
            {
                case MappedKeys.Up:
                    up = key;
                    break;
                case MappedKeys.Down:
                    down = key;
                    break;
                case MappedKeys.Left:
                    left = key;
                    break;
                case MappedKeys.Right:
                    right = key;
                    break;
                case MappedKeys.A:
                    a = key;
                    break;
                case MappedKeys.B:
                    b = key;
                    break;
                case MappedKeys.Start:
                    start = key;
                    break;
                default:
                    throw new Exception("Mapped key not recongized.");
            }
        }


        public static bool Check(MappedKeys key)
        {
            return Input.Check(GetMapping(key));
        }

        public static bool Pressed(MappedKeys key)
        {
            return Input.Pressed(GetMapping(key));
        }

        public static bool Released(MappedKeys key)
        {
            return Input.Released(GetMapping(key));
        }











        #endregion

    }

    public enum MappedKeys
    {
        Up,
        Down,
        Left,
        Right,
        A,
        B,
        Start
    }

}
