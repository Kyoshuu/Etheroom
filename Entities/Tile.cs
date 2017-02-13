using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Etheroom.Utility;
using System;
using Etheroom.Entities.Components;
using System.Collections.Generic;

namespace Etheroom.Entities
{
    public enum TileType
    {
        NORMAL,
        SOLID
    }

    public class Tile : Entity
    {
        public TileType tileType;

        SpriteRenderer spriteRenderer;
        VectorRef position;

        Color border;

        public Tile(Texture2D texture, Vector3 indexes, TileType tileType)
        {
            this.tileType = tileType;

            spriteRenderer = GetComponent<SpriteRenderer>();
            position = GetComponent<Transformation>().Position;

            spriteRenderer.Texture = texture;

            position.SetPosition(new Vector2(
                indexes.X * 32 - indexes.Y * 32,
                indexes.X * 19 + indexes.Y * 19 - 26 * indexes.Z
                ));

            Indexes = indexes;

            GetComponent<Collider>().collisionBox = texture.Bounds;

            GetComponent<Collider>().AddOnCollisionMethod(OnCollision);

            if (indexes.Z == 1)
                border = Color.Blue;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Indexes.Z >= GameManager.Instance.CurrentScene.CurrentFloor - 1 && Indexes.Z < GameManager.Instance.CurrentScene.CurrentFloor + 1)
                spriteRenderer.Draw(spriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            if (Indexes.Z >= GameManager.Instance.CurrentScene.CurrentFloor - 1 && Indexes.Z < GameManager.Instance.CurrentScene.CurrentFloor + 1)
                spriteRenderer.Draw(spriteBatch, offset);
        }

        public override void DebugDraw(SpriteBatch spriteBatch)
        {
            if (border != null && Indexes.Z >= GameManager.Instance.CurrentScene.CurrentFloor - 1 && Indexes.Z < GameManager.Instance.CurrentScene.CurrentFloor + 1)
                Primitives.drawRectangle(GetComponent<Collider>().GetPositionedCollisionBox(), border, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void AddComponents()
        {
            AddComponent(new Transformation(this));
            AddComponent(new SpriteRenderer(this));
            AddComponent(new Collider(this));
        }

        private void OnCollision(Entity other)
        {
            if (tileType == TileType.SOLID && other.GetType() != typeof(Tile))
            {
                List<float> distances = new List<float>(4);

                VectorRef thisTransformationPosition = GetComponent<Transformation>().Position;
                VectorRef tileTransformationPosition = other.GetComponent<Transformation>().Position;

                Movement movement = other.GetComponent<Movement>();

                Rectangle thisCollider = GetComponent<Collider>().GetPositionedCollisionBox();
                Rectangle otherCollider = other.GetComponent<Collider>().GetPositionedCollisionBox();

                distances.Add(Math.Abs(thisCollider.Bottom - otherCollider.Top));
                distances.Add(Math.Abs(thisCollider.Left - otherCollider.Right));
                distances.Add(Math.Abs(thisCollider.Top - otherCollider.Bottom));
                distances.Add(Math.Abs(thisCollider.Right - otherCollider.Left));

                List<float> sortedDistances = new List<float>(distances);
                sortedDistances.Sort();

                int shortestIndex = distances.IndexOf(sortedDistances[0]);

                switch (shortestIndex)
                {
                    case 0:
                        movement.Move(new Vector2(0, distances[0]));
                        break;
                    case 1:
                        movement.Move(new Vector2(-distances[1], 0));
                        break;
                    case 2:
                        movement.Move(new Vector2(0, -distances[2]));
                        break;
                    case 3:
                        movement.Move(new Vector2(distances[3], 0));
                        break;
                }
            }
        }
    }
}
