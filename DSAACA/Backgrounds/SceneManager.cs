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
    class SceneManager : DrawableGameComponent
    {
        #region Properties
        public Stack<Scene> Scenes;
        private SceneMenu mainMenu;
        private Game gameRoot;
        #endregion

        #region Constructor
        public SceneManager(Game game) : base(game)
        {
            game.Components.Add(this);
            gameRoot = game;
            CreateScenes();
            Scenes.Push(mainMenu);
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            foreach (Scene scene in Scenes)
            {
                scene.Update();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Scene scene in Scenes)
            {
                scene.Draw(gameRoot);
            }

            base.Draw(gameTime);
        }

        private void CreateScenes()
        {
            mainMenu = new SceneMenu(
                GameRoot.TextureResource["Backgrounds\\menu_main"],
                GameRoot.MusicResource["Backgrounds\\menu_main"],
                Keys.Enter);
        }
        #endregion
    }
}
