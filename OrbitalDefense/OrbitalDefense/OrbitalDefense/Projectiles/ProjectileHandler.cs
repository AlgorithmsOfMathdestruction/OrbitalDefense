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

        private LinkedList<MovingTurretShot> shots = new LinkedList<MovingTurretShot>();

        public ProjectileHandler(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            pos = new Vector2(2, 2);
            font = Game.Content.Load<SpriteFont>("kootenay");
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void RegisterShot(MovingTurretShot shot)
        {
            shots.AddLast(shot);
            shot.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < shots.Count; ++i)
            //foreach (MovingTurretShot s in shots)
            {
                MovingTurretShot s = shots.ElementAt(i);
                s.Update(gameTime);
                if (s.lifetime_ms <= 0.0f)
                {
                    shots.Remove(s);
                    s.Dispose();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawString(font, String.Format("Shots:{0}", shots.Count), pos, Color.Yellow);

            foreach (MovingTurretShot s in shots)
                s.Draw(gameTime,spriteBatch);

            spriteBatch.End();
        }
    }
}
