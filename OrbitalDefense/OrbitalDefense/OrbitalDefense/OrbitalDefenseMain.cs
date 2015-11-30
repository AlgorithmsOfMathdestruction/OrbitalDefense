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
using OrbitalDefense.Projectiles;
using OrbitalDefense.Turrets;

namespace OrbitalDefense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class OrbitalDefenseMain : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        DrawableGameComponent bg;
        DrawableGameComponent fps;
        DrawableGameComponent planet;
        DrawableGameComponent planetHud;
        DrawableGameComponent planetBasis;

        TurretDefaultBullet turret;

        DrawableGameComponent shotHandler;

        public OrbitalDefenseMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            new Background(this as Game);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            shotHandler = new ProjectileHandler(this);
            shotHandler.Initialize();

            bg = new Background(this as Game);
            bg.Initialize();
            fps = new FPSCounter(this as Game);
            fps.Initialize();
            planet = new HomePlanet(this as Game);
            planet.Initialize();
            planetHud = new PlanetStatusDisplay(this as Game, planet as HomePlanet);
            planetHud.Initialize();
            planetBasis = new PlanetBasis(this as Game);
            planetBasis.Initialize();

            turret = new TurretDefaultBullet(this as Game, new Vector2(800, 600), shotHandler as ProjectileHandler);
            turret.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
                turret.LanchTurret();

            // TODO: Add your update logic here

            shotHandler.Update(gameTime);
            bg.Update(gameTime);
            planet.Update(gameTime);
            planetBasis.Update(gameTime);
            turret.Update(gameTime);

            planetHud.Update(gameTime);
            fps.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            shotHandler.Draw(gameTime);
            bg.Draw(gameTime);
            planet.Draw(gameTime);
            planetBasis.Draw(gameTime);
            turret.Draw(gameTime);

            planetHud.Draw(gameTime);
            fps.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
