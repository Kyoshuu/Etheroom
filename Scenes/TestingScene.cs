using Etheroom.Content;
using Etheroom.Content.Loaders;
using Etheroom.Entities;
using Etheroom.Entities.Components;
using Etheroom.Rooms;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Etheroom.Scenes
{
    public class TestingScene : Scene
    {
        Player player;
        Room room;

        public override void Initialize()
        {
            room = new Room();
            player = new Player(TextureLoader.Instance.GetTexture("char"), new Vector3(2, 3, 1));

            offset = new Vector2(-32 + 12 * 32, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (GameManager.Instance.scrolling)
                manageInput();

            room.Update();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            room.Draw(spriteBatch, offset);
            player.Draw(spriteBatch, offset);

            if (GameManager.Instance.drawDebug)
            {
                room.DebugDraw(spriteBatch);
                player.DebugDraw(spriteBatch);
            }
        }

        private void manageInput()
        {
            if (Etheroom.inputManager.IsKeyDown(Keys.W))
            {
                offset.Y++;
            }
            if (Etheroom.inputManager.IsKeyDown(Keys.S))
            {
                offset.Y--;
            }
            if (Etheroom.inputManager.IsKeyDown(Keys.A))
            {
                offset.X++;
            }
            if (Etheroom.inputManager.IsKeyDown(Keys.D))
            {
                offset.X--;
            }
        }
    }
}
