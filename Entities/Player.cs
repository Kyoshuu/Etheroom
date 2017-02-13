using System;
using Etheroom.Entities.Components;
using Etheroom.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Etheroom.Entities
{
    public class Player : Entity
    {
        Vector2 previousPosition;
        VectorRef position;

        SpriteRenderer spriteRenderer;

        Movement movementComponent;

        public Player(Texture2D texture, Vector3 indexes)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            position = GetComponent<Transformation>().Position;

            spriteRenderer.Texture = texture;

            position.SetPosition(new Vector2(
                indexes.X * 32 - indexes.Y * 32,
                indexes.X * 19 + indexes.Y * 19 - 26 * indexes.Z
                ));

            Indexes = indexes;

            movementComponent = GetComponent<Movement>();

            GetComponent<Collider>().collisionBox = texture.Bounds;

            GetComponent<Collider>().AddOnCollisionMethod(OnCollision);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteRenderer.Draw(spriteBatch);
            Primitives.drawRectangle(GetComponent<Collider>().GetPositionedCollisionBox(), Color.Blue, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            previousPosition = position.Vector2;

            if(!GameManager.Instance.scrolling)
            {
                manageInput();
            }
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteRenderer.Draw(spriteBatch, offset);
            Primitives.drawRectangle(GetComponent<Collider>().GetPositionedCollisionBox(), Color.Blue, spriteBatch);
        }

        private void manageInput()
        {
            if (Etheroom.inputManager.IsKeyDown(Keys.W))
            {
                movementComponent.Move(0, -2);
            }
            if (Etheroom.inputManager.IsKeyDown(Keys.S))
            {
                movementComponent.Move(0, 2);
            }
            if (Etheroom.inputManager.IsKeyDown(Keys.A))
            {
                movementComponent.Move(-2, 0);
            }
            if (Etheroom.inputManager.IsKeyDown(Keys.D))
            {
                movementComponent.Move(2, 0);
            }
        }

        protected override void AddComponents()
        {
            AddComponent(new Transformation(this));
            AddComponent(new SpriteRenderer(this));
            AddComponent(new Collider(this));
            AddComponent(new Movement(this));
        }
            
        private void OnCollision(Entity other)
        {
            
        }
    }
}
