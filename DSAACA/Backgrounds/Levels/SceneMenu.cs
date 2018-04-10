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

namespace DSAACA.Backgrounds.Levels
{
    class SceneMenu : Scene
    {
        #region Properties
        private MenuUI ui;
        #endregion

        #region Constructor
        public SceneMenu(Sprite texture, Song bgm, Keys key)
            : base(texture, bgm, key)
        {
            ui = new MenuUI(GameRoot.TextureResource["ui_arrow"], 
                new Vector2(0,0), CreateMenuItems());
        }

        #endregion

        #region Methods
        public override void Update()
        {

        }

        public override void Draw(SpriteBatch sp)
        {

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
