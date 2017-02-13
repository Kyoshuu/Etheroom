using Etheroom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etheroom.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Etheroom.Scenes
{
    public abstract class Scene
    {
        public List<Entity> Entities { get; private set; }

        public int CurrentFloor { get; private set; }

        public Vector2 offset;

        public Scene()
        {
            Entities = new List<Entity>();

            CurrentFloor = 1;
        }

        public abstract void Initialize();

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Entity entity in Entities)
            {
                if (entity.Indexes.Z >= CurrentFloor - 1 && entity.Indexes.Z < CurrentFloor + 1)
                    entity.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Entity entity in Entities)
            {
                if (entity.Indexes.Z >= CurrentFloor - 1 && entity.Indexes.Z < CurrentFloor + 1)
                    entity.Draw(spriteBatch);
            }
        }
    }
}
