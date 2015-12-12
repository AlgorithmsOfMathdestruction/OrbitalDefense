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


namespace OrbitalDefense.Enemy
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class BaseEnemy : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Vector2 screenPosition;
        float rotation;
        float targetRotation;
        float rotationSpeed;
        float rotationDeaccelerationFactor;
        float scale;
        float radius;

        SpriteBatch spriteBatch;
        Texture2D shipTexture, SimpleTexture;

        float speed;
        float maxSpeed;
        float acceleration;
        Vector2 moveDirection;
        Vector2 targetDirection;
        Vector2 center;

        Vector2 flightTarget;
        DrawableGameComponent attackTarget;
       

        public BaseEnemy(Game game, SpriteBatch batch, float scale, Vector2 position)
            : base(game)
        {
            screenPosition = position;
            this.scale = scale;
            spriteBatch = batch;

            speed = 0f;
            maxSpeed = 500f;
            acceleration = 150;
            rotationSpeed = 2;
            rotationDeaccelerationFactor = 0.6f;
            rotation = 0;
            targetRotation = MathHelper.ToRadians(45);

            flightTarget = new Vector2(100,100);
            moveDirection = flightTarget - screenPosition;
            moveDirection.Normalize();
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

            shipTexture = Game.Content.Load<Texture2D>("enemy");
            center = new Vector2(shipTexture.Width / 2, shipTexture.Height /2);
            radius = shipTexture.Width;

            SimpleTexture = new Texture2D(GraphicsDevice, 10, 10, false, SurfaceFormat.Color);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);

            if (speed <= maxSpeed)
                speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                speed = maxSpeed;

            if (TargetDist() < radius)
            {
                Random r = new Random(DateTime.Now.Millisecond);
                flightTarget = new Vector2(400f + (float)r.NextDouble() * 1000f,200f + (float)r.NextDouble() * 600f);
            }
            //    moveDirection = flightTarget - screenPosition;
            //    moveDirection.Normalize();

            //    if (moveDirection.X < 0f)
            //        targetRotation = (float)Math.Acos(-moveDirection.Y) / moveDirection.Length();
            //    else
            //        targetRotation = 2f * (float)Math.PI - (float)Math.Acos(-moveDirection.Y) / moveDirection.Length();

            //    targetRotation = Mod360(targetRotation);
            //}

            targetDirection = flightTarget - screenPosition;
            targetDirection.Normalize();

            //if (targetDirection.X < 0.0f)
            //    targetRotation = (float)Math.Acos(-targetDirection.Y) / targetDirection.Length();
            //else
            //    targetRotation = 2f * (float)Math.PI - (float)Math.Acos(-targetDirection.Y) / targetDirection.Length();

            //targetRotation = (float)Math.Atan2(targetDirection.Y, targetDirection.X);

            targetRotation = getRotation(0, -1, targetDirection.X, targetDirection.Y);

            targetRotation = Mod360(targetRotation);

            if (Math.Abs(targetRotation - rotation) > 0.01f)
            {
                if (Math.Abs(targetRotation - rotation) > 0.05f)
                    speed *= 1.0f - rotationDeaccelerationFactor * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if ((targetRotation - rotation) > 0f)
                    rotation += rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    rotation -= rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            rotation = Mod360(rotation);

            moveDirection.X = (float)-Math.Sin(rotation);
            moveDirection.Y = (float)Math.Cos(rotation);
            moveDirection.Normalize();      

            screenPosition += moveDirection * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private float getRotation(float x, float y, float x2, float y2)
        {
            float adj = x - x2;
            float opp = y - y2;
            float tan = opp / adj;
            float res = MathHelper.ToDegrees((float)Math.Atan2(opp, adj));
            res = (res - 180) % 360;
            if (res < 0) { res += 360; }
            res = MathHelper.ToRadians(res);
            return res;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Draw(shipTexture, screenPosition, null, Color.White, -rotation, new Vector2(shipTexture.Width / 2.0f, shipTexture.Height / 2.0f), scale, SpriteEffects.None, 0.1f);
        }

        private float TargetDist()
        {
            return (flightTarget - screenPosition).Length();
        }

        private float Mod360(float rot)
        {
            rot = rot % (2*MathHelper.Pi);
            if (rot < 0.0)
                rot = 2 * MathHelper.Pi - rot;
            return rot;
        }
    }
}
