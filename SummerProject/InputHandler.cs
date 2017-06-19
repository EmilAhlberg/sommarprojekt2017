using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public static class InputHandler
    {
        public static Point mPosition { get { return Mouse.GetState().Position; } }
        static KeyboardState kState;
        static KeyboardState pKState;
        static MouseState mState;
        static MouseState pMState;


        static InputHandler()
        {
            kState = Keyboard.GetState();
            pKState = kState;
            mState = Mouse.GetState();
            pMState = mState;
        }

        /// <summary>
        /// To be ran at the end/start of the Update-method in Game1
        /// </summary>
        public static void UpdatePreviousState()
        {
            pKState = kState;
            kState = Keyboard.GetState();
            pMState = mState;
            mState = Mouse.GetState();
        }

        public static bool isPressed(Keys key)
        {
            return kState.IsKeyDown(key);
        }

        public static bool isReleased(Keys key)
        {
            return kState.IsKeyUp(key);
        }

        public static bool isJustPressed(Keys key)
        {
            return kState.IsKeyDown(key) && !pKState.IsKeyDown(key);
        }

        public static bool isJustReleased(Keys key)
        {
            return kState.IsKeyUp(key) && !pKState.IsKeyUp(key);
        }

        public static bool isPressed(MouseButton mb)
        {
            switch (mb)
            {
                case MouseButton.LEFT:
                    return mState.LeftButton == ButtonState.Pressed;
                case MouseButton.RIGHT:
                    return mState.RightButton == ButtonState.Pressed;
                case MouseButton.MIDDLE:
                    return mState.MiddleButton == ButtonState.Pressed;
                default:
                    throw new NotImplementedException();
            }
        }

        public static bool isReleased(MouseButton mb)
        {
            switch (mb)
            {
                case MouseButton.LEFT:
                    return mState.LeftButton == ButtonState.Released;
                case MouseButton.RIGHT:
                    return mState.RightButton == ButtonState.Released;
                case MouseButton.MIDDLE:
                    return mState.MiddleButton == ButtonState.Released;
                default:
                    throw new NotImplementedException();
            }
        }

        public static bool isJustPressed(MouseButton mb)
        {
            switch (mb)
            {
                case MouseButton.LEFT:
                    return mState.LeftButton == ButtonState.Pressed && pMState.LeftButton != ButtonState.Pressed;
                case MouseButton.RIGHT:
                    return mState.RightButton == ButtonState.Pressed && pMState.LeftButton != ButtonState.Pressed;
                case MouseButton.MIDDLE:
                    return mState.MiddleButton == ButtonState.Pressed && pMState.LeftButton != ButtonState.Pressed;
                default:
                    throw new NotImplementedException();
            }
        }

        public static bool isJustReleased(MouseButton mb)
        {
            switch (mb)
            {
                case MouseButton.LEFT:
                    return mState.LeftButton == ButtonState.Released && pMState.LeftButton != ButtonState.Released;
                case MouseButton.RIGHT:
                    return mState.RightButton == ButtonState.Released && pMState.LeftButton != ButtonState.Released;
                case MouseButton.MIDDLE:
                    return mState.MiddleButton == ButtonState.Released && pMState.LeftButton != ButtonState.Released;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
