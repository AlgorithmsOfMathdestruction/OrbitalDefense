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
    public abstract class BaseTurretGun : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Vector2 screenPosition;

        public SpriteBatch spriteBatch;
        public Texture2D gun;
        public ProjectileHandlerGroup shotHandlerGroup;

        public float rotation;
        public float scale;

        public List<BaseTurret> turrets = new List<BaseTurret>();
        public List<Vector2> turretOrigins = new List<Vector2>(); 

        private Vector2 rotateBuffer = new Vector2(0, 0); // sin = X , cos = Y

        public BaseTurretGun(Game game, SpriteBatch batch, Vector2 screenPosition, float scale, ProjectileHandlerGroup shotHandlerGroup) : base(game)
        {
            this.spriteBatch = batch;
            this.shotHandlerGroup = shotHandlerGroup;
            this.scale = scale;
            rotation = 0;
            this.screenPosition = screenPosition;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float roatationMove = -100.0f * (float)gameTime.ElapsedGameTime.TotalMinutes;
            rotation += roatationMove;

            rotateBuffer.X = (float)Math.Sin(-rotation);
            rotateBuffer.Y = (float)Math.Cos(rotation);

            for (int i = 0; i < turrets.Count; ++i)
            {
                turrets[i].rotation += roatationMove;
                turrets[i].screenPosition = screenPosition + new Vector2(turretOrigins[i].X * rotateBuffer.Y - turretOrigins[i].Y * rotateBuffer.X, turretOrigins[i].X * rotateBuffer.X + turretOrigins[i].Y * rotateBuffer.Y);

                turrets[i].Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Draw(gun, screenPosition, null, Color.White, -rotation, new Vector2(gun.Width / 2.0f, gun.Height / 2.0f), scale, SpriteEffects.None, 0.1f);

            foreach (BaseTurret t in turrets)
                t.Draw(gameTime);
        }

        protected void Destroy()
        {
            shotHandlerGroup.RequestDestroyHandler(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Destroy();
        }

        public void ActivateFire()
        {
            foreach (BaseTurret t in turrets)
                t.activateFire();
        }

        public void DeactivateFire()
        {
            foreach (BaseTurret t in turrets)
                t.deactivateFire();
        }
    }
}
