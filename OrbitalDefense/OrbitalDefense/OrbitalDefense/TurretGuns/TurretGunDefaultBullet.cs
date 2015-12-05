using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbitalDefense.Projectiles;
using OrbitalDefense.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrbitalDefense.TurretGuns
{
    public class TurretGunDefaultBullet : BaseTurretGun
    {
        public TurretGunDefaultBullet(Game game, SpriteBatch batch, Vector2 screenPosition, float scale, ProjectileHandlerGroup shotHandlerGroup) : base(game, batch, screenPosition, scale, shotHandlerGroup)
        {
            turrets.Add(new TurretDefaultBullet(Game, spriteBatch, scale, shotHandlerGroup.GetNewHandler(this)));
            turrets.Last().Initialize();
            turrets.Add(new TurretDefaultBullet(Game, spriteBatch, scale, shotHandlerGroup.GetNewHandler(this)));
            turrets.Last().Initialize();
            rotation = 0;
            turretOrigins.Add(new Vector2(7 , -4) *scale);
            turretOrigins.Add(new Vector2(-7 , -4)* scale);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
       
            gun = Game.Content.Load<Texture2D>("turretGunDefault");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
