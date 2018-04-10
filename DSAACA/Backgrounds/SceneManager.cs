using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DSAACA.Backgrounds.Levels;

namespace DSAACA.Backgrounds
{
    class SceneManager : GameComponent
    {
        #region Properties
        public Stack<Scene> Scenes;
        #endregion

        #region Constructor
        public SceneManager(Game game) : base(game)
        {
            CreateScenes();
            
        }
        #endregion

        #region Methods
        private void CreateScenes()
        {
            SceneMenu menuScene = new SceneMenu(new Vector2(0, 0),
                GameRoot.TextureResource["menu_main"],
                GameRoot.MusicResource["menu_main"],
                Keys.Enter,
                GameRoot.TextureResource["ui_arrow"],
                new Vector2(0,0), 
        }
        #endregion
    }
}
