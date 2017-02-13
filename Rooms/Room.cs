using Etheroom.Content;
using Etheroom.Content.Loaders;
using Etheroom.Entities;
using Etheroom.Entities.Components;
using Etheroom.Utility;
using Etheroom.Utility.Noise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Etheroom.Rooms
{
    public class Room
    {
        Random rand = new Random();
        
        //Zrobic chunki z tablica 3 wymiarowa :O
        private List<Tile> tiles;

        Texture2D floor;
        Texture2D dirt;

        double freq = 8;

        public int Levels { get; set; }

        public List<Tile> Tiles { get { return tiles; } }

        public List<int> solidIDs;

        OpenSimplexNoise openSimplexNoise;

        int[] noiseValues;

        int chunkSize = 16;

        public Room()
        {
            tiles = new List<Tile>();

            Levels = 5;

            floor = TextureLoader.Instance.GetTexture("floor");
            dirt = TextureLoader.Instance.GetTexture("dirt");

            openSimplexNoise = new OpenSimplexNoise();

            noiseValues = new int[chunkSize * chunkSize];

            for (int i = 0; i < chunkSize * chunkSize; i++)
            {
                noiseValues[i] = (int)((openSimplexNoise.Eval((i % chunkSize) / freq, (i / chunkSize) / freq) + 1) / 2 * Levels);
            }

            InitializeRoom();
        }

        public void InitializeRoom()
        {
            //for (int i = 0; i < chunkSize * chunkSize; i++)
            //{
            //    for (int j = 0; j <= noiseValues[i]; j++)
            //    {
            //        tiles.Add(new Tile(floor, new Vector3(i % chunkSize, i / chunkSize, j), TileType.NORMAL));
            //    }
            //}

            for (int i = 0; i < chunkSize * chunkSize; i++)
            {
                tiles.Add(new Tile(floor, new Vector3(i % chunkSize, i / chunkSize, noiseValues[i]), TileType.SOLID));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile t in tiles)
            {
                t.Draw(spriteBatch);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            foreach (Tile t in tiles)
            {
                t.Draw(spriteBatch, offset);
                spriteBatch.DrawString(Etheroom.arial, $"Frequency: {freq}", Vector2.Zero, Color.White);
            }
        }

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            foreach(Tile t in tiles)
            {
                t.DebugDraw(spriteBatch);
            }
        }

        public void Update()
        {
            HandleInput();
        }

        private void GenerateRoom()
        {
            //Usunac automatyczne dodawanie entity - powoduje wycieki POTWORNE
            foreach (Tile t in tiles)
                GameManager.Instance.CurrentScene.RemoveEntity(t);

            tiles.Clear();

            openSimplexNoise = new OpenSimplexNoise(rand.Next());

            for (int i = 0; i < chunkSize * chunkSize; i++)
            {
                noiseValues[i] = (int)((openSimplexNoise.Eval((i % chunkSize) / freq, (i / chunkSize) / freq) + 1) / 2 * 5);
            }

            InitializeRoom();
        }

        private void HandleInput()
        {
            if (Etheroom.inputManager.IsKeyPressed(Keys.Space))
            {
                GenerateRoom();
            }

            if (Etheroom.inputManager.IsKeyDown(Keys.Up))
            {
                freq += 0.05;
            }
            else if (Etheroom.inputManager.IsKeyDown(Keys.Down))
            {
                freq -= 0.05;
            }
        }
    }
}
