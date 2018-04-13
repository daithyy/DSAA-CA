using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using DSAACA.Backgrounds;
using DSAACA.Components;
using DSAACA.Entities;
using InputManager;

namespace DSAACA
{
    public class GameRoot : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int _width = 1280;
        private int _height = 720;

        public static Dictionary<string, SpriteFont> FontResource;

        private SceneManager sceneManager;

        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = _width;
            graphics.PreferredBackBufferHeight = _height;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            IsFixedTimeStep = true;

            Window.Title = "Data Structures & Algorithms - Easter CA";
            Window.AllowAltF4 = false; 
        }

        protected override void Initialize()
        {
            Window.Position = new Point(
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) -
                (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) -
                (graphics.PreferredBackBufferHeight / 2));

            new InputEngine(this);
            //Helper.GameRoot = this;
            Helper.GraphicsDevice = this.GraphicsDevice;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(spriteBatch);

            FontResource = Loader.ContentLoad<SpriteFont>(Content, "Assets\\Fonts");

            sceneManager = new SceneManager(this);
            sceneManager.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           


            base.Draw(gameTime);
        }

        #region Methods

        #endregion
    }
}
