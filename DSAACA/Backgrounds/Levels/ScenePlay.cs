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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(new Point(0), playAreaSize.ToPoint()), Color.White);
            player.Draw(spriteBatch);
        }

        private void Init()
        {
            player = new Player(SceneManager.TextureResource["player"], new Vector2(100, 100), 1);
            backgroundTexture = SceneManager.BackgroundResourcePlay["bg_ground"];
        }

        public void InitCamera(Game game)
        {
            currentCamera = new Camera(game, new Vector2(0,0), playAreaSize);
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
