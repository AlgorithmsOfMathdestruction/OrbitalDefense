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
    public class HomePlanet : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D planet;
        private Texture2D planetdamage, planetshield;

        float rotSpeed;
        float rotation;
        float damage;
        float damagePulseRange;
        float damagePulse;
        float damageRaisesPerMinute;
        bool damagePulseRaise = true;

        public float ShieldStatus { get { return MathHelper.Clamp(shield / shieldmax, 0.0f, 1.0f); } }
        public float HealthStatus { get { return MathHelper.Clamp(hitpoints / hitpointsmax,0.0f, 1.0f); } }

        private float hitpoints, hitpointsmax;
        private float regenrationPerMinute;
        private float shield, shieldmax, shieldmin;
        private float shieldRegenerationPerMinute;

        public HomePlanet(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();

            rotSpeed = 1.0f;
            rotation = 0.0f;

            damagePulseRange = 0.01f;
            damage = 0.0f;
            damagePulse = 0.0f;
            damageRaisesPerMinute = 10;

            hitpoints = 1000.0f;
            hitpointsmax = 10000.0f;
            regenrationPerMinute = 2000.0f;
            shield = 0.0f;
            shieldmax = 500.0f;
            shieldmin = -50.0f;
            shieldRegenerationPerMinute = 500.0f;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);

            float minutes = (float)gameTime.ElapsedGameTime.TotalMinutes;

            rotation =  (rotation + rotSpeed * (float)gameTime.ElapsedGameTime.TotalMinutes) % 360.0f;

            if (damagePulseRaise)
                damagePulse += damageRaisesPerMinute * minutes;
            else
                damagePulse -= damageRaisesPerMinute * minutes;
            if (damagePulse < -damagePulseRange)
                damagePulseRaise = true;
            if (damagePulse > damagePulseRange)
                damagePulseRaise = false;

            hitpoints = MathHelper.Clamp(hitpoints + minutes * regenrationPerMinute, -10.0f, hitpointsmax);
            damage = MathHelper.Clamp(1.0f - HealthStatus - damagePulse, 0.0f, 0.8f);
            shield = MathHelper.Clamp(shield + minutes * shieldRegenerationPerMinute, shieldmin, shieldmax);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            planet = Game.Content.Load<Texture2D>("planet");
            planetdamage = Game.Content.Load<Texture2D>("planetdamage");
            planetshield = Game.Content.Load<Texture2D>("planetshield");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.Draw(planet, new Rectangle(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2, planet.Width, planet.Height), null,
                new Color(1.0f, 1.0f, 1.0f, 1.0f), (float) rotation, new Vector2(planet.Width / 2.0f, planet.Height / 2.0f), SpriteEffects.None, 0f);

            spriteBatch.Draw(planetdamage, new Rectangle(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2, planet.Width, planet.Height), null,
                new Color(damage, damage, damage, damage), (float)rotation, new Vector2(planetdamage.Width / 2.0f, planetdamage.Height / 2.0f), SpriteEffects.None, 0f);

            spriteBatch.Draw(planetshield, new Rectangle(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2, planetshield.Width, planetshield.Height), null,
                new Color(ShieldStatus, ShieldStatus, ShieldStatus, ShieldStatus), 0.0f, new Vector2(planetshield.Width / 2.0f, planetshield.Height / 2.0f), SpriteEffects.None, 0f);    
    
            spriteBatch.End();
        }
    }
}
