using System;
using Microsoft.Xna.Framework;
using Etheroom.Utility;

namespace Etheroom.Entities.Components
{
    public class Transformation : Component
    {
        public VectorRef Position { get; set; }
        public float Rotation { get; set; }

        public Transformation(Entity owner) : base(owner)
        {
            Position = new VectorRef();
            Rotation = 0f;
        }
    }
}
