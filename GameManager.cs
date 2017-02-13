using Etheroom.Entities;
using Etheroom.Entities.Components;
using Etheroom.Input;
using Etheroom.Scenes;
using Etheroom.Utility.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etheroom
{
    public sealed class GameManager
    {
        private static GameManager instance;
        public bool scrolling = true;
        public bool drawDebug = true;

        QuadTree collisionTree;
        List<Entity> entitiesWithCollider;

        public static GameManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GameManager();
                }

                return instance;
            }
        }

        public Scene CurrentScene { get; set; }

        private GameManager()
        {
            collisionTree = new QuadTree(0, new Rectangle(0, 0, 64 * 12, 64 * 8), 10, 20);
            entitiesWithCollider = new List<Entity>();
        }

        public void Initialize()
        {
            CurrentScene = new TestingScene();
            CurrentScene.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            UpdateCollisionTree();

            ManageCollisions();

            ManageInput();

            CurrentScene.Update(gameTime);
        }

        private void ManageInput()
        {
            if (Etheroom.inputManager.IsKeyPressed(Keys.Tab))
                scrolling = !scrolling;
        }

        private void UpdateCollisionTree()
        {
            collisionTree.Clear();
            entitiesWithCollider.Clear();

            foreach (Entity entity in CurrentScene.Entities)
            {
                if (entity.HasComponent<Collider>())
                {
                    collisionTree.Insert(entity);
                    entitiesWithCollider.Add(entity);
                }
            }
        }

        private void ManageCollisions()
        {
            foreach(Entity entityWithCollider in entitiesWithCollider)
            {
                Collider entityCollider = entityWithCollider.GetComponent<Collider>();
                List<Entity> possiblyColliding = new List<Entity>();

                collisionTree.Retrieve(entityWithCollider, possiblyColliding);

                List<Entity> collidingWith = new List<Entity>();
                collidingWith = possiblyColliding.FindAll(entity => entity != entityWithCollider && entity.Indexes.Z == entityWithCollider.Indexes.Z && entity.GetComponent<Collider>().Intersects(entityCollider));

                entityCollider.UpdateCollidingWithPreviously();
                entityCollider.collidingWith = collidingWith;

                List<Entity> entitiesEnteredCollision = collidingWith.FindAll(entity => entity != entityWithCollider && !entityCollider.collidingWithPreviously.Contains(entity));

                foreach (Entity entity in entitiesEnteredCollision)
                {
                    entityWithCollider.GetComponent<Collider>().OnCollisionEnter(entity);
                }

                List<Entity> entitiesColliding = collidingWith.FindAll(entity => entity != entityWithCollider && entityCollider.collidingWithPreviously.Contains(entity));

                foreach (Entity entity in entitiesColliding)
                {
                    entityWithCollider.GetComponent<Collider>().OnCollision(entity);
                }

                List<Entity> entitiesQuitColliding = entityCollider.collidingWithPreviously.FindAll(entity => entity != entityWithCollider && !collidingWith.Contains(entity));

                foreach (Entity entity in entitiesQuitColliding)
                {
                    entityWithCollider.GetComponent<Collider>().OnCollisionExit(entity);
                }
            }
        }
    }
}
