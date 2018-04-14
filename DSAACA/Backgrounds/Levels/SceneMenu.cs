using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using DSAACA.UI;
using DSAACA.Entities;
using DSAACA.Components;

namespace DSAACA.Backgrounds.Levels
{
    class SceneMenu : Scene
    {
        #region Properties
        public MenuUI UserInterface;
        private SpriteFont systemFont;
        private const int SPACING = 100;        
        #endregion

        #region Constructor
        public SceneMenu(Queue<Texture2D> textures, Queue<Texture2D> pointerTextures, Song bgm, Keys activateKey, Keys escapeKey)
            : base(textures, bgm, activateKey, escapeKey)
        {
            Init();

            UserInterface = new MenuUI(pointerTextures, CreateMenuItems(), activateKey);
        }

        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
            UserInterface.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(Position.ToPoint() + Camera.CamPos.ToPoint(), 
                new Point(
                    Helper.GraphicsDevice.Viewport.Bounds.Width,
                    Helper.GraphicsDevice.Viewport.Bounds.Height)), Color.White);

            UserInterface.Draw(spriteBatch);
        }

        private void Init()
        {
            systemFont = GameRoot.FontResource["systemFont"];
            MediaPlayer.Play(BackingTrack);
        }

        private List<MenuItem> CreateMenuItems()
        {
            int viewportCenterWidth = (Helper.GraphicsDevice.Viewport.Width / 2);
            int viewportCenterHeight = (Helper.GraphicsDevice.Viewport.Height / 2);

            string play = "Play";
            string highScore = "High Scores";
            string quit = "Quit";

            return new List<MenuItem>
            {
                new MenuItem(play.ToUpper(), systemFont, Color.White,
                new Vector2(viewportCenterWidth -
                (systemFont.MeasureString(play).X / 2),
                viewportCenterHeight
                )),
                new MenuItem(highScore.ToUpper(), systemFont, Color.White,
                new Vector2(viewportCenterWidth -
                (systemFont.MeasureString(highScore).X / 2),
                viewportCenterHeight + SPACING
                )),
                new MenuItem(quit.ToUpper(), systemFont, Color.White,
                new Vector2(viewportCenterWidth -
                (systemFont.MeasureString(quit).X / 2),
                viewportCenterHeight + SPACING * 2
                ))
            };
        }
        #endregion
    }
}
