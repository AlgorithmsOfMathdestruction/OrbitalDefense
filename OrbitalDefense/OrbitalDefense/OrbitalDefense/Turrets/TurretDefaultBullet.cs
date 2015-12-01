﻿using System;
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
        public TurretDefaultBullet(Game game, Vector2 screenPosition, ProjectileHandler shotHandler) : base(game, screenPosition, ref shotHandler)
        {
            rotation = 0.0f;

            supportedAmmoTypes.Add(Ammo.AmmoDamageType.Bullet);
            currentAmmoType = new AmmoDefaultBullet(game);
            currentAmmoType.Initialize();
            
            turnSpeed = 0.1f;
            fireRate_spm = 1000.0f;
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
            localEntrancePoint = new Vector2(0, turret.Width/2.0f);
        }

        

    }
}