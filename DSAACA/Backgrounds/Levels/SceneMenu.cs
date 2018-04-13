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
        public SceneMenu(Queue<Texture2D> textures, Queue<Texture2D> pointerTextures, Song bgm, Keys key)
            : base(textures, bgm, key)
        {
            ui = new MenuUI(pointerTextures, 
                new Vector2(0,0), CreateMenuItems(), key);

            Init();
        }

        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
            ui.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(Position.ToPoint(), 
                new Point(
                    Helper.GraphicsDevice.Viewport.Bounds.Width,
                    Helper.GraphicsDevice.Viewport.Bounds.Height)), Color.White);

            ui.Draw(spriteBatch);
        }

        private void Init()
        {
            Active = true;
            MediaPlayer.Play(BackingTrack);
        }

        private List<MenuItem> CreateMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem("Play",
                    //GameRoot.TextureResource["ui_arrow"],
                    GameRoot.FontResource["systemFont"], Color.White, new Vector2(Helper.GraphicsDevice.Viewport.Width / 2 - 100, 300)),
                new MenuItem("High Scores",
                    //GameRoot.TextureResource["ui_arrow"],
                    GameRoot.FontResource["systemFont"], Color.White, new Vector2(Helper.GraphicsDevice.Viewport.Width / 2, 300)),
                new MenuItem("Quit",
                    //GameRoot.TextureResource["ui_arrow"],
                    GameRoot.FontResource["systemFont"], Color.White, new Vector2(Helper.GraphicsDevice.Viewport.Width / 2 + 100, 300))
            };
        }
        #endregion
    }
}
