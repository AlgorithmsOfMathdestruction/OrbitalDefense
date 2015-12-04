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
    public class BaseTurretGun : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Vector2 screenPosition;

        public SpriteBatch spriteBatch;
        public Texture2D gun;
        public ProjectileHandlerGroup shotHandlerGroup;

        public float rotation;

        public Vector2 gun1Origin, gun2Origin;

        public BaseTurret gun1;
        public BaseTurret gun2;

        private Vector2 rotateBuffer = new Vector2(0, 0); // sin = X , cos = Y

        public BaseTurretGun(Game game, SpriteBatch batch, ProjectileHandlerGroup shotHandlerGroup) : base(game)
        {
            this.spriteBatch = batch;
            this.shotHandlerGroup = shotHandlerGroup;
            gun1 = new TurretDefaultBullet(Game, spriteBatch, shotHandlerGroup.GetNewHandler(this));
            gun1.Initialize();
            gun2 = new TurretDefaultBullet(Game, spriteBatch, shotHandlerGroup.GetNewHandler(this));
            gun2.Initialize();
            rotation = 0;
            screenPosition = new Vector2(800, 600);
            gun1Origin = new Vector2(7, 0);
            gun2Origin = new Vector2(-7, 0);
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

            float roatationMove = -100.0f * (float)gameTime.ElapsedGameTime.TotalMinutes;
            rotation += roatationMove;

            rotateBuffer.X = (float)Math.Sin(-rotation);
            rotateBuffer.Y = (float)Math.Cos(rotation);

            gun1.rotation += roatationMove;
            gun1.screenPosition = screenPosition + new Vector2(gun1Origin.X * rotateBuffer.Y - gun1Origin.Y * rotateBuffer.X, gun1Origin.X * rotateBuffer.X + gun1Origin.Y * rotateBuffer.Y);
            gun2.rotation += roatationMove;
            gun2.screenPosition = screenPosition + new Vector2(gun2Origin.X * rotateBuffer.Y - gun2Origin.Y * rotateBuffer.X, gun2Origin.X * rotateBuffer.X + gun2Origin.Y * rotateBuffer.Y);

            gun1.Update(gameTime);
            gun2.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Draw(gun, screenPosition, null, Color.White, -rotation, new Vector2(gun.Width / 2.0f, gun.Height / 2.0f), 1.0f, SpriteEffects.None, 0.1f);

            gun1.Draw(gameTime);
            gun2.Draw(gameTime);
        }

        void Destroy()
        {
            shotHandlerGroup.RequestDestroyHandler(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Destroy();
            //turret = null;
            //shotHandlerGroup = null;
            //gun1.Dispose();
            //gun1 = null;
        }
    }
}
