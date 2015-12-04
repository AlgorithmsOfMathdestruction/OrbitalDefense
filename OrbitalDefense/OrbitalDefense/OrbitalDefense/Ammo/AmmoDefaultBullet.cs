using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrbitalDefense.Ammo
{
    public class AmmoDefaultBullet : BaseAmmo
    {
        public AmmoDefaultBullet(Game game) : base(game)
        {
            baseSpeed = 250.0f;
            baseDamage = 10;
            baseLifetime_ms = 5000;
            baseAcceleration = 250.0f;

            ammoType = AmmoDamageType.Bullet;
        }

        public override void Initialize()
        {
            base.Initialize();

            texture = Game.Content.Load<Texture2D>("ammoDefaultBullet");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
