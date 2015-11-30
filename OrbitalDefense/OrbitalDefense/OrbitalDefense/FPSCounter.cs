using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrbitalDefense
{
    class FPSCounter : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont _spr_font;

        double updateRate = 0;

        public FPSCounter(Game game) : base(game)
        {
            _spr_font = Game.Content.Load<SpriteFont>("kootenay");
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            updateRate = 1.0 / gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(_spr_font, string.Format("FPS={0:F1}", 1.0/gameTime.ElapsedGameTime.TotalSeconds),
                new Vector2(Game.Window.ClientBounds.Width - 80.0f, Game.Window.ClientBounds.Height - 20.0f), Color.Yellow);
            spriteBatch.DrawString(_spr_font, string.Format("Upd={0:F1}", updateRate),
                new Vector2(Game.Window.ClientBounds.Width - 80.0f, Game.Window.ClientBounds.Height - 40.0f), Color.Yellow);
            spriteBatch.End();
        }
    }
}
