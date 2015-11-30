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
            HealthBarMaxLength = 150f;
            ShieldBarMaxLength = 150f;
            HealthBarLength = 0f;
            ShieldBarLength = 0f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            HealthBar = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            HealthBar.SetData(new[] { Color.Red });
            HealthBarBackground = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            HealthBarBackground.SetData(new[] { Color.LightGray });
            HealthBarPos = new Vector2(1760, 5);
            ShieldBar = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            ShieldBar.SetData(new[] { Color.LightBlue });
            ShieldBarBackground = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            ShieldBarBackground.SetData(new[] { Color.LightGray });
            HealthBar.SetData(new[] { Color.Red });
            ShieldBarPos = new Vector2(1760, 22);

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
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();


            //TODO Wieso taucht die Bar nicht auf ?
            spriteBatch.Draw(HealthBarBackground, new Rectangle((int)HealthBarPos.X, (int)HealthBarPos.Y, (int)HealthBarMaxLength+2, 14), Color.LightGray);
            spriteBatch.Draw(ShieldBarBackground, new Rectangle((int)ShieldBarPos.X, (int)ShieldBarPos.Y, (int)ShieldBarMaxLength+2, 14), Color.LightGray);
            spriteBatch.Draw(HealthBar, new Rectangle((int)HealthBarPos.X+1, (int)HealthBarPos.Y+1, (int)HealthBarLength, 12), Color.Red);
            spriteBatch.Draw(ShieldBar, new Rectangle((int)ShieldBarPos.X+1, (int)ShieldBarPos.Y+1, (int)ShieldBarLength, 12), Color.LightBlue);

            spriteBatch.End();
        }
    }
}
