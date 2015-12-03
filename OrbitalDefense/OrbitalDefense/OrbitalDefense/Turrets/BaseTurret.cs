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
using OrbitalDefense.Projectiles;

namespace OrbitalDefense.Turrets
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class BaseTurret : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Vector2 screenPosition;

        protected static BaseAmmo currentAmmoType;
        public BaseAmmo CurrentAmmoType { get { return currentAmmoType; } }

        public SpriteBatch spriteBatch;
        public Texture2D turret;
        public ProjectileHandler shotHandler;

        public float rotation;
        public Vector2 localEntrancePoint;

        public Vector2 EntrancePoint { get { return screenPosition; } } // ToDo: mit turret textur und rotation syncen

        public List<AmmoDamageType> supportedAmmoTypes = new List<AmmoDamageType>();
        public float turnSpeed;
        public float fireRate_spm;
        public float accuracy_per;

        private float cooldown; 

        public int ammoMax;
        public int ammoCount;
        public bool isAmmoUnlimited = false;

        public float ammoSpeedModifier;
        public float ammoDamageModifier;
        public float ammoAccelerationModifier;
        public float ammoLifetimeModifier;

        public BaseTurret(Game game, Vector2 screenPosition, ref ProjectileHandler shotHandler)
            : base(game)
        {
            // TODO: Construct any child components here
            this.screenPosition = screenPosition;
            this.shotHandler = shotHandler;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);

            if (cooldown > 0.0)
                cooldown -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.Draw(turret, new Rectangle((int)screenPosition.X,(int)screenPosition.Y, turret.Width, turret.Height), null,
                new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.0f, new Vector2(turret.Width / 2.0f, turret.Height / 2.0f), SpriteEffects.None, 0.1f);

            spriteBatch.End();
        }

        public void LanchTurret()
        {
            if (cooldown <= 0.0f)
            {
                shotHandler.RegisterShot(new MovingTurretShot(Game, this));
                cooldown = 60000.0f / fireRate_spm;
            }
        }
    }
}
