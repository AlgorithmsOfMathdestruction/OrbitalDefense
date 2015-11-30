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
using OrbitalDefense.Ammo;
using OrbitalDefense.Turrets;

namespace OrbitalDefense.Projectiles
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MovingTurretShot : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private ProjectileHandler shotHandler;
        public SpriteBatch spriteBatch;

        public BaseAmmo ammo;
        public Vector2 moveDirection;
        public Vector2 position;

        public float maxSpeed;
        public float currentSpeed;
        public float acceleration;
        public float lifetime_ms;
        public float damage;

        public MovingTurretShot(Game game, BaseTurret emitter)
            : base(game)
        {
            // TODO: Construct any child components here
            ammo = emitter.CurrentAmmoType;
            shotHandler = emitter.shotHandler;

            maxSpeed = ammo.baseSpeed * emitter.ammoSpeedModifier;
            acceleration = ammo.baseAcceleration * emitter.ammoAccelerationModifier;
            lifetime_ms = ammo.baseLifetime_ms * emitter.ammoLifetimeModifier;
            damage = ammo.baseDamage * emitter.ammoDamageModifier;

            position = emitter.EntrancePoint;
            moveDirection = new Vector2(1.0f,0.0f); // ToDo: Mit drehung des turrets syncen

            currentSpeed = 0.0f;

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);

            if (lifetime_ms > 0.0f)
            {
                lifetime_ms -= gameTime.ElapsedGameTime.Milliseconds;

                if (currentSpeed > maxSpeed)
                    currentSpeed = maxSpeed;
                else
                    currentSpeed += (float)gameTime.ElapsedGameTime.TotalSeconds * acceleration;

                position += moveDirection * currentSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                shotHandler.UnregisterShot(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.Draw(ammo.texture, new Rectangle((int)position.X, (int)position.Y, ammo.texture.Width, ammo.texture.Height), null, new Color(1f, 1f, 1f, 1f), 0f, new Vector2(ammo.texture.Width / 2, ammo.texture.Height), SpriteEffects.None, 0.01f);

            spriteBatch.End();
        }
    }
}
