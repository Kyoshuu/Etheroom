using Etheroom.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Etheroom.Entities
{
    public abstract class Entity
    {
        List<Component> components;

        public bool active;

        public Vector3 Indexes { get; protected set; }

        //temp
        public int ID;
        private static int index = 0;

        public int Width
        {
            get
            {
                int width = 0;

                if (HasComponent<SpriteRenderer>())
                {
                    width = GetComponent<SpriteRenderer>().Texture.Width;
                }

                return width;
            }
        }

        public int Height
        {
            get
            {
                int height = 0;

                if (HasComponent<SpriteRenderer>())
                {
                    height = GetComponent<SpriteRenderer>().Texture.Height;
                }

                return height;
            }
        }

        public Vector2 CenterPosition
        {
            get
            {
                Vector2 centerPosition = GetComponent<Transformation>().Position.Vector2;

                if (HasComponent<SpriteRenderer>())
                {
                    centerPosition = centerPosition + new Vector2(Width / 2, Height / 2);
                }

                return centerPosition;
            }
        }

        public Entity()
        {
            ID = index++;
            GameManager.Instance.CurrentScene.AddEntity(this);
            Initialize();
            AddComponents();
            InitializeComponents();
        }

        private void Initialize()
        {
            components = new List<Component>();
            active = true;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
            {
                if (typeof(T).Equals(component.GetType()))
                    return component as T;
            }

            return null;
        }

        public bool HasComponent<T>() where T : Component
        {
            return components.Exists((component) => typeof(T).Equals(component.GetType()));
        }

        private void ComponentsDo(Action<Component> method)
        {
            foreach (Component component in components)
            {
                method(component);
            }
        }

        private void UpdateComponents(GameTime gameTime)
        {
            ComponentsDo((Component component) => component.Update(gameTime));
        }


        private void InitializeComponents()
        {
            ComponentsDo((Component component) => component.Initialize());
        }

        protected abstract void AddComponents();

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset) { }

        public virtual void Update(GameTime gameTime)
        {
            UpdateComponents(gameTime);
        }

        public virtual void DebugDraw(SpriteBatch spriteBatch)
        {

        }

        public virtual void DebugDraw(SpriteBatch spriteBatch, Vector2 offset)
        {

        }
    }
}
