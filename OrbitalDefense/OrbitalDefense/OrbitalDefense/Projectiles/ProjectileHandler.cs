using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrbitalDefense.Projectiles
{
    public class ProjectileHandler : DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        private List<MovingTurretShot> shots = new List<MovingTurretShot>();
        private List<MovingTurretShot> unregisterBuffer = new List<MovingTurretShot>();

        public ProjectileHandler(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void RegisterShot(MovingTurretShot shot)
        {
            shots.Add(shot);
            shot.Initialize();
        }

        public void UnregisterShot(MovingTurretShot shot)
        {
            unregisterBuffer.Add(shot);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (MovingTurretShot s in unregisterBuffer)
                shots.Remove(s);
            unregisterBuffer.Clear();

            foreach (MovingTurretShot s in shots)
                s.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("kootenay"), String.Format("Shots:{0}", shots.Count), new Vector2(2, 2), Color.Yellow); ;

            spriteBatch.End();

            foreach (MovingTurretShot s in shots)
                s.Draw(gameTime);


        }
    }
}
