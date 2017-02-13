using Microsoft.Xna.Framework.Graphics;
using Etheroom.Content.Loaders;
using Etheroom.Utility;
using Microsoft.Xna.Framework;
using Etheroom.Utility.Noise;

namespace Etheroom.Scenes
{
    public class RoomGeneratorTestingScene : Scene
    {
        Texture2D pixel;
        double[] noises;

        OpenSimplexNoise openSimplexNoise;

        public override void Initialize()
        {
            pixel = TextureLoader.Instance.GetTexture("pixel");
            noises = new double[500 * 500];

            openSimplexNoise = new OpenSimplexNoise(5679223870);

            for(int y = 0; y < 500; y++)
            {
                for (int x = 0; x < 500; x++)
                {
                    noises[500 * y + x] = openSimplexNoise.Eval(x / 2.63, y / 2.63);
                    //noises[500 * y + x] = PerlinNoise.Noise(x / 2.63, y / 2.63, 0.7436);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < 500; y++)
            {
                for (int x = 0; x < 500; x++)
                {
                    int grayScale = (int)(noises[500 * y + x] * 255);
                    spriteBatch.Draw(pixel, new Vector2(x, y), new Color(grayScale, grayScale, grayScale));
                }
            }
        }
    }
}
