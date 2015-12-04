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
using OrbitalDefense.TurretGuns;
using OrbitalDefense.Projectiles;

namespace OrbitalDefense
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlanetBasis : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Vector2 position;

        private SpriteBatch spriteBatch;
        private Texture2D planetBasis;

        BaseTurretGun gun1;
        BaseTurretGun gun2;
        BaseTurretGun gun3;
        BaseTurretGun gun4;

        public PlanetBasis(Game game, SpriteBatch batch, ProjectileHandlerGroup projectileHandlerGroup)
            : base(game)
        {
            // TODO: Construct any child components here
            spriteBatch = batch;
            position = new Vector2( 960,540);
            gun1 = new BaseTurretGun(Game, spriteBatch, position + new Vector2(0,-20), projectileHandlerGroup);
            gun2 = new BaseTurretGun(Game, spriteBatch, position + new Vector2(-20, 0), projectileHandlerGroup);
            gun3 = new BaseTurretGun(Game, spriteBatch, position + new Vector2(0, 20), projectileHandlerGroup);
            gun4 = new BaseTurretGun(Game, spriteBatch, position + new Vector2(20, 0), projectileHandlerGroup);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();

            gun1.Initialize();
            gun2.Initialize();
            gun3.Initialize();
            gun4.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            planetBasis = Game.Content.Load<Texture2D>("planetbase");
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);

            gun1.Update(gameTime);
            gun2.Update(gameTime);
            gun3.Update(gameTime);
            gun4.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Draw(planetBasis, position, null, Color.White,
               0.0f, new Vector2(planetBasis.Width / 2.0f, planetBasis.Height / 2.0f), 1.0f,  SpriteEffects.None, 0.1f);

            gun1.Draw(gameTime);
            gun2.Draw(gameTime);
            gun3.Draw(gameTime);
            gun4.Draw(gameTime);

        }

        public void FireGuns()
        {
            gun1.FireGuns();
            gun2.FireGuns();
            gun3.FireGuns();
            gun4.FireGuns();
        }
    }
}
