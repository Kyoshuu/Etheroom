using Etheroom.Content;
using Etheroom.Content.Loaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Etheroom.Utility
{
    public class Primitives
    {
        public static void drawRectangle(Rectangle rectangle, Color color, SpriteBatch spriteBatch)
        {
            Texture2D pixel = TextureLoader.Instance.GetTexture("pixel");

            spriteBatch.Draw(pixel, new Vector2(rectangle.Left, rectangle.Top), new Rectangle(0, 0, 1, 1), color, 0f, Vector2.Zero, new Vector2(rectangle.Width, 1), SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Vector2(rectangle.Left, rectangle.Top), new Rectangle(0, 0, 1, 1), color, 0f, Vector2.Zero, new Vector2(1, rectangle.Height), SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Vector2(rectangle.Left, rectangle.Bottom), new Rectangle(0, 0, 1, 1), color, 0f, Vector2.Zero, new Vector2(rectangle.Width, 1), SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Vector2(rectangle.Right, rectangle.Top), new Rectangle(0, 0, 1, 1), color, 0f, Vector2.Zero, new Vector2(1, rectangle.Height), SpriteEffects.None, 0);

        }

        public static void drawFilledRectangle(Rectangle rectangle, Color color, SpriteBatch spriteBatch)
        {
            Texture2D pixel = TextureLoader.Instance.GetTexture("pixel");

            spriteBatch.Draw(pixel, new Vector2(rectangle.Left, rectangle.Top), new Rectangle(0, 0, 1, 1), color, 0f, Vector2.Zero, new Vector2(rectangle.Width, rectangle.Height), SpriteEffects.None, 0);
        }
    }
}
