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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace DSAACA.Backgrounds
{
    class SceneManager : DrawableGameComponent
    {
        #region Properties
        public Stack<Scene> Scenes;
        private SceneMenu mainMenu;
        private ScenePlay play;
        private SceneHighScore highScore;
        private Game gameRoot;

        public static Dictionary<string, SoundEffect> AudioResource;
        public static Dictionary<string, Song> MusicResource;
        private Queue<Texture2D> mainMenuTextures;
        private Queue<Texture2D> highScoreTextures;
        private Queue<Texture2D> pointerTextures;
        #endregion

        #region Constructor
        public SceneManager(Game game) : base(game)
        {
            game.Components.Add(this);
            gameRoot = game;

            Scenes = new Stack<Scene>();
        }
        #endregion

        #region Methods
        public void LoadContent(ContentManager content)
        {
            AudioResource = Loader.ContentLoad<SoundEffect>(content, "Assets\\Sounds");
            MusicResource = Loader.ContentLoad<Song>(content, "Assets\\Music");
            mainMenuTextures = Loader.ContentLoadQueue<Texture2D>(content, "Assets\\Backgrounds\\MainMenu");
            pointerTextures = Loader.ContentLoadQueue<Texture2D>(content, "Assets\\Sprites\\Select");

            CreateScenes();
            PushScenes();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.Active && scene != null)
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
                if (scene.Active && scene != null)
                    scene.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CreateScenes()
        {
            mainMenu = new SceneMenu(mainMenuTextures, pointerTextures, MusicResource["bgm"], Keys.Enter);
        }

        private void PushScenes()
        {
            Scenes.Push(mainMenu);
        }
        #endregion
    }
}
