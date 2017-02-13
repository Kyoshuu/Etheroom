using Etheroom.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etheroom.Entities.Components
{
    public class Movement : Component
    {
        VectorRef ownerPosition;
        public Vector2 Velocity { get; private set; }

        public Movement(Entity owner) : base(owner)
        {
            ownerPosition = owner.GetComponent<Transformation>().Position;
        }

        public void Move(Vector2 velocity)
        {
            Velocity += velocity;
        }

        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        public void Stop()
        {
            Velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            ownerPosition.SetPosition(ownerPosition.Vector2 + Velocity);
            Stop();
        }
    }
}
