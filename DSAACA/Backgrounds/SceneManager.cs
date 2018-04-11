using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DSAACA.Backgrounds.Levels;
using Microsoft.Xna.Framework.Graphics;
using DSAACA.Components;

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
            Scenes = new Stack<Scene>();
            CreateScenes();
            Scenes.Push(mainMenu);
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.Active)
                    scene.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)gameRoot.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Begin(
                SpriteSortMode.Immediate, 
                BlendState.AlphaBlend, 
                SamplerState.PointClamp, null, null, null, Camera.CurrentCameraTranslation);

            foreach (Scene scene in Scenes)
            {
                if (scene.Active)
                    scene.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CreateScenes()
        {
            mainMenu = new SceneMenu(
                GameRoot.TextureResource["mainMenu"],
                GameRoot.MusicResource["bgm"],
                Keys.Enter);
        }
        #endregion
    }
}
