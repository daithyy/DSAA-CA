﻿using System;
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
        private Queue<Texture2D> Textures;
        #endregion

        #region Constructor
        public SceneMenu(Texture2D texture, Song bgm, Keys key)
            : base(texture, bgm, key)
        {
            ui = new MenuUI(, 
                new Vector2(0,0), CreateMenuItems());

            Init();
        }

        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            ui.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(
                new Point((int)Position.X, (int)Position.Y), 
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
                    GameRoot.TextureResource["ui_arrow"],
                    GameRoot.FontResource["systemFont"], Color.White, new Vector2(0, 0)),
                new MenuItem("High Scores",
                    GameRoot.TextureResource["ui_arrow"],
                    GameRoot.FontResource["systemFont"], Color.White, new Vector2(0, 0)),
                new MenuItem("Quit",
                    GameRoot.TextureResource["ui_arrow"],
                    GameRoot.FontResource["systemFont"], Color.White, new Vector2(0, 0))
            };
        }
        #endregion
    }
}
