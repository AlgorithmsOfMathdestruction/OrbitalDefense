using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OrbitalDefense
{
    public class Background : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D layer1, layer2, layer3;

        int swapPerMinute = 10;
        double changeValue;
        private bool toogleLayer = false;
        private double layer1Opacity, layer2Opacity, layer3Opacity;

        public Background(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            layer1Opacity = 1.0;
            layer2Opacity = 1.0;
            layer3Opacity = 0.2; 
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            layer1 = Game.Content.Load<Texture2D>("stars_a");
            layer2 = Game.Content.Load<Texture2D>("stars_b");
            layer3 = Game.Content.Load<Texture2D>("stars_c");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            changeValue = swapPerMinute * gameTime.ElapsedGameTime.TotalMinutes;

            if (toogleLayer)
            {
                layer2Opacity += changeValue;
                layer3Opacity -= changeValue;
            }
            else
            {
                layer2Opacity -= changeValue;
                layer3Opacity += changeValue;
            }

            if (layer2Opacity < 0.2 || layer3Opacity < 0.2)
                toogleLayer = !toogleLayer;
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);


            spriteBatch.Draw(layer2, new Rectangle(0, 0, layer2.Width, layer2.Height),
                new Color(255, 255, 255, (byte)MathHelper.Clamp((int)(layer2Opacity * 255), 0, 255)));
            spriteBatch.Draw(layer3, new Rectangle(0, 0, layer3.Width, layer3.Height),
                new Color(255, 255, 255, (byte)MathHelper.Clamp((int)(layer3Opacity * 255), 0, 255)));

            spriteBatch.End();
        }
    }
}
