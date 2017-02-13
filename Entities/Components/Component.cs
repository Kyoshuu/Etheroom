using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etheroom.Entities.Components
{
    public abstract class Component
    {
        protected Entity owner;

        public Component(Entity owner)
        {
            this.owner = owner;
        }

        public virtual void Initialize() { }
        public virtual void Update(GameTime gameTime) { }
    }
}
