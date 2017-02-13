using Etheroom.Content;
using Etheroom.Content.Loaders;
using Etheroom.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Etheroom
{
    public class Etheroom : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static InputManager inputManager;

        TextureLoader textureLoader;

        GameManager gameManager;

        public static SpriteFont arial;

        public Etheroom()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = 64 * 12;
            graphics.PreferredBackBufferHeight = 64 * 8;
            graphics.ApplyChanges();

            arial = Content.Load<SpriteFont>("arial");

            textureLoader = TextureLoader.Instance;
            textureLoader.InitializeTextures(Content);

            gameManager = GameManager.Instance;
            gameManager.Initialize();

            inputManager = new InputManager();

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            inputManager.Update();

            gameManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            gameManager.CurrentScene.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
