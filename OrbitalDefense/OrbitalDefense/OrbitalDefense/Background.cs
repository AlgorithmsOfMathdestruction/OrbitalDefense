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

        private double swapPerMinuteLayer2 = 2.0;
        private double swapPerMinuteLayer3 = 0.2;
        private double l2RotPerMinute;

        private double layer2Opacity, layer3Opacity;
        private double l2upper, l2lower, l3upper, l3lower;
        private bool l2forward = true, l3forward = true;

        private double l2rotation = 0.0;

        public Background(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            layer2Opacity = 0.4;
            layer3Opacity = 0.3;

            l2upper = 0.5; l2lower = 0.2;
            l3upper = 0.3; l3lower = 0.1;

            l2RotPerMinute = 0.2;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            layer1 = Game.Content.Load<Texture2D>("stars1");
            layer2 = Game.Content.Load<Texture2D>("stars2");
            layer3 = Game.Content.Load<Texture2D>("stars3");
        }

        public override void Update(GameTime gameTime)
        {
            float minutes = (float)gameTime.ElapsedGameTime.TotalMinutes;

            if (l2forward)
                layer2Opacity += swapPerMinuteLayer2 * minutes;
            else
                layer2Opacity -= swapPerMinuteLayer2 * minutes;

            if (l3forward)
                layer3Opacity += swapPerMinuteLayer3 * minutes;
            else
                layer3Opacity -= swapPerMinuteLayer3 * minutes;

            if (layer2Opacity <= l2lower)
                l2forward = true;
            if (layer2Opacity >= l2upper)
                l2forward = false;

            if (layer3Opacity <= l3lower)
                l3forward = true;
            if (layer3Opacity >= l3upper)
                l3forward = false;

            l2rotation = (l2rotation + l2RotPerMinute * minutes) % 360.0;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            spriteBatch.Draw(layer1, new Rectangle(0, 0, layer1.Width, layer1.Height),
                new Color(1.0f - (float)layer3Opacity, 1.0f, 1.0f, 1.0f));
            spriteBatch.Draw(layer3, new Rectangle(0, 0, layer3.Width, layer3.Height),
                new Color(1.0f, 1.0f - (float)layer3Opacity, 1.0f - (float)layer3Opacity, (float)layer3Opacity));
            spriteBatch.Draw(layer2, new Rectangle(layer2.Width / 2, layer2.Height / 2, layer2.Width, layer2.Height), null,
                new Color(1.0f, 1.0f, 1.0f - (float)layer2Opacity, (float)layer2Opacity), (float)l2rotation, new Vector2(layer2.Width / 2.0f, layer2.Height / 2.0f), SpriteEffects.None, 0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
