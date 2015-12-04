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
        //public SpriteBatch spriteBatch;

        public BaseAmmo ammo;
        public Vector2 moveDirection;
        public Vector2 position;
        public Vector2 center;
        public float rotation;

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

            maxSpeed = ammo.baseSpeed * emitter.ammoSpeedModifier;
            acceleration = ammo.baseAcceleration * emitter.ammoAccelerationModifier;
            lifetime_ms = ammo.baseLifetime_ms * emitter.ammoLifetimeModifier;
            damage = ammo.baseDamage * emitter.ammoDamageModifier;

            position = emitter.EntrancePoint;
            center = new Vector2(ammo.texture.Width / 2, ammo.texture.Height);

            rotation = emitter.rotation;
            moveDirection = new Vector2((float)-Math.Sin(rotation), (float)-Math.Cos(rotation));
            // ToDo: Mit drehung des turrets syncen

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

            //spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            float time_ms = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            float time_s = (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);

            if (lifetime_ms > 0.0f)
            {
                lifetime_ms -= time_ms;

                if (currentSpeed > maxSpeed)
                    currentSpeed = maxSpeed;
                else
                    currentSpeed += time_s * acceleration;

                position += moveDirection * currentSpeed * time_s;
                if (position.X > Game.Window.ClientBounds.Width || position.X < 0 || position.Y > Game.Window.ClientBounds.Height || position.Y < 0)
                    lifetime_ms = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime);

            batch.Draw(ammo.texture, position, null, Color.White, -rotation, center, 1.0f, SpriteEffects.None, 0.1f);
        }
    }
}
