
using Microsoft.Xna.Framework.Input;

namespace Etheroom.Input
{
    public class InputManager
    {
        KeyboardState previousKeyboardState, currentKeyboardState;

        public InputManager()
        {
        }

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
    }
}
