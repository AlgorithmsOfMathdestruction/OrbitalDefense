using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace OrbitalDefense
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlanetStatusDisplay : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        Texture2D HealthBar, HealthBarBackground;
        Texture2D ShieldBar, ShieldBarBackground;

        private float HealthBarLength, HealthBarMaxLength;
        private double ShieldBarLength, ShieldBarMaxLength;
        private Vector2 HealthBarPos, ShieldBarPos;

        private HomePlanet planet;

        public PlanetStatusDisplay(Game game, HomePlanet planet)
            : base(game)
        {
            // TODO: Construct any child components here
            this.planet = planet;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            HealthBarMaxLength = 100f;
            ShieldBarMaxLength = 100f;
            HealthBarLength = 0f;
            ShieldBarLength = 0f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            HealthBar = new Texture2D(GraphicsDevice, (int)HealthBarLength+1, 10);
            HealthBarBackground = new Texture2D(GraphicsDevice, (int)HealthBarMaxLength, 12);
            HealthBarPos = new Vector2(1700, 5);
            ShieldBar = new Texture2D(GraphicsDevice, (int)ShieldBarLength+1, 10);
            ShieldBarBackground = new Texture2D(GraphicsDevice, (int)ShieldBarMaxLength, 12);
            ShieldBarPos = new Vector2(1700, 18);

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);

            HealthBarLength = HealthBarMaxLength * planet.HealthStatus;
            ShieldBarLength = ShieldBarMaxLength * planet.ShieldStatus;
            HealthBar = new Texture2D(Game.GraphicsDevice, (int)HealthBarLength+1, 10);
            ShieldBar = new Texture2D(Game.GraphicsDevice, (int)ShieldBarLength+1, 10);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();


            //TODO Wieso taucht die Bar nicht auf ?
            spriteBatch.Draw(HealthBarBackground, new Rectangle(0,0, HealthBarBackground.Width, HealthBarBackground.Height), null,
                new Color(1.0f, 0.0f, 0.0f, 1.0f), 0, HealthBarPos, SpriteEffects.None, 0.5f);
            //spriteBatch.Draw(HealthBarBackground, HealthBarPos, Color.Brown);
            spriteBatch.Draw(ShieldBarBackground, ShieldBarPos, Color.Brown);
            spriteBatch.Draw(HealthBar, HealthBarPos, Color.Red);
            spriteBatch.Draw(ShieldBar, ShieldBarPos, Color.LightBlue);

            spriteBatch.End();

        }
    }
}
