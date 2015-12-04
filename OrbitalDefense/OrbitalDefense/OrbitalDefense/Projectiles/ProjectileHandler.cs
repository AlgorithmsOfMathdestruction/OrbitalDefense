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
        SpriteFont font;
        Vector2 pos;

        public List<MovingTurretShot> shots = new List<MovingTurretShot>();

        public ProjectileHandler(Game game, SpriteBatch batch) : base(game)
        {
            this.spriteBatch = batch;
        }

        public override void Initialize()
        {
            base.Initialize();

            pos = new Vector2(2, 2);
            font = Game.Content.Load<SpriteFont>("kootenay");
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void RegisterShot(MovingTurretShot shot)
        {
            shot.Initialize();
            shots.Add(shot);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < shots.Count; ++i)
            {
                MovingTurretShot s = shots.ElementAt(i);
                s.Update(gameTime);
                if (s.lifetime_ms <= 0.0f)
                    shots.Remove(s);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.DrawString(font, String.Format("Shots:{0}", shots.Count), pos, Color.Yellow);

            foreach (MovingTurretShot s in shots)
                s.Draw(gameTime,spriteBatch);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();

            shots.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            //spriteBatch = null;
            //font = null;
        }
    }
}
