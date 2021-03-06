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

        TurretGunDefaultBullet gun1;
        TurretGunDefaultBullet gun2;
        TurretGunDefaultBullet gun3;
        TurretGunDefaultBullet gun4;

        public PlanetBasis(Game game, SpriteBatch batch, ProjectileHandlerGroup projectileHandlerGroup)
            : base(game)
        {
            // TODO: Construct any child components here
            spriteBatch = batch;
            position = new Vector2( 960,540);
            gun1 = new TurretGunDefaultBullet(Game, spriteBatch, position + new Vector2(0,-25), 0.7f, projectileHandlerGroup);
            gun2 = new TurretGunDefaultBullet(Game, spriteBatch, position + new Vector2(-25, 0), 0.7f, projectileHandlerGroup);
            gun3 = new TurretGunDefaultBullet(Game, spriteBatch, position + new Vector2(0, 25), 0.7f, projectileHandlerGroup);
            gun4 = new TurretGunDefaultBullet(Game, spriteBatch, position + new Vector2(25, 0), 0.7f, projectileHandlerGroup);
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

        public void ActivateFire()
        {
            gun1.ActivateFire();
            gun2.ActivateFire();
            gun3.ActivateFire();
            gun4.ActivateFire();
        }

        public void DeactivateFire()
        {
            gun1.DeactivateFire();
            gun2.DeactivateFire();
            gun3.DeactivateFire();
            gun4.DeactivateFire();
        }
    }
}
