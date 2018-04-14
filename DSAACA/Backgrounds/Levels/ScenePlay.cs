using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using DSAACA.Components;
using DSAACA.Entities;

namespace DSAACA.Backgrounds.Levels
{
    class ScenePlay : Scene
    {
        #region Properties
        private SpriteFont systemFont;
        private Texture2D backgroundTexture;
        private Vector2 playAreaSize
        {
            get
            {
                return new Vector2(1920, 1080);
            }
        }
        private Camera currentCamera;
        public Player player;
        private List<Collectable> collectables;
        private const int COLLECTABLE_AMOUNT = 9;
        public const int MAX_SCORE = COLLECTABLE_AMOUNT * Collectable.SCORE_AMOUNT;
        public static int Score;
        #endregion

        #region Constructor
        public ScenePlay(Queue<Texture2D> textures, Song bgm, Keys key)
            : base(textures, bgm, key)
        {
            Init();
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            Camera.Follow(player.CentrePosition, Helper.GraphicsDevice.Viewport, currentCamera.CameraSpeed);
            player.Update(gameTime);
            player.UpdateAnimation(gameTime);
            ClampPlayer(playAreaSize);
            foreach (Collectable item in collectables)
            {
                item.UpdateAnimation(gameTime);
                item.Update(gameTime, player);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(new Point(0), playAreaSize.ToPoint()), Color.White);
            player.Draw(spriteBatch);
            foreach (Collectable item in collectables)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.DrawString(systemFont, "SCORE: " + Score,
                new Vector2((Helper.GraphicsDevice.Viewport.Width / 2) -
                (systemFont.MeasureString("SCORE: " + Score.ToString()).X / 2), 32) + Camera.CamPos, Color.White);
        }

        private void Init()
        {
            systemFont = GameRoot.FontResource["systemFont"];
            backgroundTexture = SceneManager.BackgroundResourcePlay["bg_ground"];
            player = new Player(SceneManager.TextureResource["player"], new Vector2(100, 100), 1);
            collectables = new List<Collectable>();

            for (int i = 0; i < COLLECTABLE_AMOUNT; i++)
            {
                collectables.Add(CreateCollectable());
            }
        }

        public void InitCamera(Game game)
        {
            currentCamera = new Camera(game, new Vector2(0,0), playAreaSize);
        }

        private Collectable CreateCollectable()
        {
            // Getting random x and y positions ...
            int xPosition = Camera.Random.Next(100, Helper.GraphicsDevice.Viewport.Bounds.Width - 100);
            int yPosition = Camera.Random.Next(100, Helper.GraphicsDevice.Viewport.Bounds.Height - 100);

            return new Collectable(SceneManager.TextureResource["coin"], new Vector2(xPosition, yPosition), 10);
        }

        private void ClampPlayer(Vector2 worldBounds)
        {
            player.Position = Vector2.Clamp(
                player.Position, 
                Vector2.Zero, 
                new Vector2(worldBounds.X - player.Bounds.Width, worldBounds.Y - player.Bounds.Height));
        }
        #endregion
    }
}
