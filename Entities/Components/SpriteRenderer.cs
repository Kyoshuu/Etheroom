using Etheroom.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Etheroom.Entities.Components
{
    public class SpriteRenderer : Component
    {
        public Texture2D Texture { get; set; }
        public Color tint;

        private VectorRef position;

        public SpriteRenderer(Entity owner) : base(owner)
        {
            position = owner.GetComponent<Transformation>().Position;
            tint = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position.Vector2, tint);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(Texture, position.Vector2 + offset, tint);
        }
    }
}
