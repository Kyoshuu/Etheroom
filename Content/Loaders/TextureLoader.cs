using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System;

namespace Etheroom.Content.Loaders
{
    public sealed class TextureLoader
    {
        private static TextureLoader instance;

        List<NamedTexture> textures;

        public static TextureLoader Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new TextureLoader();
                }
                return instance;
            }
        }

        private TextureLoader()
        {
            textures = new List<NamedTexture>();
        }

        public void AddTexture(string name, Texture2D texture)
        {
            textures.Add(new NamedTexture(name, texture));
        }

        public Texture2D GetTexture(string name)
        {
            foreach(NamedTexture namedTexture in textures)
            {
                if(namedTexture.name == name)
                {
                    return namedTexture.texture;
                }
            }

            return null;
        }

        internal void InitializeTextures(ContentManager content)
        {
            AddTexture("char", content.Load<Texture2D>("char"));
            AddTexture("pixel", content.Load<Texture2D>("pixel"));
            AddTexture("floor", content.Load<Texture2D>("floor"));
            AddTexture("dirt", content.Load<Texture2D>("dirt"));
        }

        private class NamedTexture
        {
            public string name;
            public Texture2D texture;

            public NamedTexture(string name, Texture2D texture)
            {
                this.name = name;
                this.texture = texture;
            }
        }
    }
}
