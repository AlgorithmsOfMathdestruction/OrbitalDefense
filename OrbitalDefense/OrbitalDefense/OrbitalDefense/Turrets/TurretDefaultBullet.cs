using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbitalDefense.Ammo;
using OrbitalDefense.Projectiles;

namespace OrbitalDefense.Turrets
{
    class TurretDefaultBullet : BaseTurret
    {
        public TurretDefaultBullet(Game game, SpriteBatch batch, float scale, ProjectileHandler shotHandler) : base(game, batch, scale, shotHandler)
        {
            rotation = 0.0f;

            supportedAmmoTypes.Add(Ammo.AmmoDamageType.Bullet);
            currentAmmoType = new AmmoDefaultBullet(game);
            currentAmmoType.Initialize();
            
            turnSpeed = 0.1f;
            fireRate_spm = 2000.0f;
            accuracy_per = 75.0f;

            ammoMax = 10000;
            ammoCount = 10000;
            isAmmoUnlimited = false;

            ammoSpeedModifier = 1.0f;
            ammoDamageModifier = 1.0f;
            ammoAccelerationModifier = 1.0f;
            ammoLifetimeModifier = 1.0f;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            turret = Game.Content.Load<Texture2D>("turretDefaultBullet");
            // center top of texture
            localEntranceOrigin = new Vector2(0, -turret.Height / 2.0f);
        }

        public override void Update(GameTime gameTime)
        {
            //rotation += 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
