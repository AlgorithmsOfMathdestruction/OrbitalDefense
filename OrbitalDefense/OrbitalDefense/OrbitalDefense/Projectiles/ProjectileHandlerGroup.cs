using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalDefense.Projectiles
{
    public class ProjectileHandlerGroup : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont font;
        Vector2 pos;

        private Dictionary<DrawableGameComponent, ProjectileHandler> projectileHandlers = new Dictionary<DrawableGameComponent, ProjectileHandler>();
        private LinkedList<DrawableGameComponent> cleanupList = new LinkedList<DrawableGameComponent>();

        public ProjectileHandlerGroup(Game game, SpriteBatch batch) : base(game)
        {
            this.spriteBatch = batch;
        }

        public ProjectileHandler GetNewHandler(DrawableGameComponent emitter)
        {
            if (projectileHandlers.ContainsKey(emitter))
                return projectileHandlers[emitter];
            else
            {
                ProjectileHandler t = new ProjectileHandler(Game, spriteBatch);
                t.Initialize();
                projectileHandlers.Add(emitter, t);
                return t;
            }
        }

        public void RequestDestroyHandler(DrawableGameComponent emitter)
        {
            cleanupList.AddLast(emitter);
        }

        public override void Initialize()
        {
            base.Initialize();

            pos = new Vector2(2, 18);
            font = Game.Content.Load<SpriteFont>("kootenay");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < cleanupList.Count; ++i)
            {
                if (projectileHandlers[cleanupList.ElementAt(i)].shots.Count == 0)
                {
                    projectileHandlers.Remove(cleanupList.ElementAt(i));
                    cleanupList.Remove(cleanupList.ElementAt(i));
                }      
            }

            foreach (DrawableGameComponent emitter in projectileHandlers.Keys)
            {
                projectileHandlers[emitter].Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (DrawableGameComponent emitter in projectileHandlers.Keys)
            {
                projectileHandlers[emitter].Draw(gameTime);
            }

            spriteBatch.DrawString(font, String.Format("Handlers:{0}", projectileHandlers.Count), pos, Color.Yellow);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            //spriteBatch = null;
            //font = null;
        }
    }
}
