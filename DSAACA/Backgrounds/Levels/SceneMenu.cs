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
        private MenuUI ui;
        #endregion

        #region Constructor
        public SceneMenu(Texture2D texture, Song bgm, Keys key)
            : base(texture, bgm, key)
        {
            ui = new MenuUI(GameRoot.TextureResource["ui_arrow"], 
                new Vector2(0,0), CreateMenuItems());

            Active = true;
        }

        #endregion

        #region Methods
        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(
                new Point((int)Position.X, (int)Position.Y), 
                new Point(
                    Helper.GraphicsDevice.Viewport.Bounds.Width,
                    Helper.GraphicsDevice.Viewport.Bounds.Height)), Color.White);
        }

        private void Init()
        {
            MediaPlayer.Play(BackingTrack);
        }

        private List<MenuItem> CreateMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem("Play",
                    GameRoot.TextureResource["ui_play"],
                    GameRoot.FontResource["systemFont"], Color.White, new Vector2(0, 0)),
                new MenuItem("High Scores",
                    GameRoot.TextureResource["ui_highScore"],
                    GameRoot.FontResource["systemFont"], Color.White, new Vector2(0, 0)),
                new MenuItem("Quit",
                    GameRoot.TextureResource["ui_quit"],
                    GameRoot.FontResource["systemFont"], Color.White, new Vector2(0, 0))
            };
        }
        #endregion
    }
}
