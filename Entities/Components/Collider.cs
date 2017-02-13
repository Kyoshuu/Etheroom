using Etheroom.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Etheroom.Entities.Components
{
    public class Collider : Component
    {
        public Action<Entity> OnCollisionEnter;
        public Action<Entity> OnCollisionExit;
        public Action<Entity> OnCollision;

        public List<Entity> collidingWithPreviously;
        public List<Entity> collidingWith;

        public Rectangle collisionBox;

        public Collider(Entity owner, Rectangle collisionBox) : this(owner)
        {
            this.collisionBox = collisionBox;
        }

        public Collider(Entity owner) : base(owner)
        {
            collidingWithPreviously = new List<Entity>();
            collidingWith = new List<Entity>();
            
            OnCollisionEnter = OnCollision = OnCollisionExit = (entity) => { };
        }

        public void AddOnCollisionEnterMethod(Action<Entity> OnCollisionEnter)
        {
            this.OnCollisionEnter = OnCollisionEnter;
        }

        public Rectangle GetPositionedCollisionBox()
        {
            VectorRef ownerPosition = owner.GetComponent<Transformation>().Position;
            Movement ownerMovement = owner.GetComponent<Movement>();

            if(ownerMovement == null)
            {
                return new Rectangle((int)(ownerPosition.X + GameManager.Instance.CurrentScene.offset.X), (int)(ownerPosition.Y + GameManager.Instance.CurrentScene.offset.Y), collisionBox.Width, collisionBox.Height);
            }

            return new Rectangle((int)(ownerPosition.X + ownerMovement.Velocity.X + GameManager.Instance.CurrentScene.offset.X), (int)(ownerPosition.Y + ownerMovement.Velocity.Y + GameManager.Instance.CurrentScene.offset.Y), collisionBox.Width, collisionBox.Height);
        }

        public bool Intersects(Collider collider)
        {
            return collider.GetPositionedCollisionBox().Intersects(GetPositionedCollisionBox());
        }

        public void AddOnCollisionExitMethod(Action<Entity> OnCollisionExit)
        {
            this.OnCollisionExit = OnCollisionExit;
        }

        public void AddOnCollisionMethod(Action<Entity> OnCollision)
        {
            this.OnCollision = OnCollision;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void UpdateCollidingWithPreviously()
        {
            collidingWithPreviously = collidingWith;
        }
    }
}
