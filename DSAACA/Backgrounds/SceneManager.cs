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
using DSAACA.UI;

namespace DSAACA.Backgrounds
{
    class SceneManager : DrawableGameComponent
    {
        #region Properties
        public Stack<Scene> Scenes;
        private Scene previousScene;
        private Scene currentScene;
        private SceneMenu mainMenu;
        private ScenePlay play;
        private SceneHighScore highScore;
        private Game gameRoot;

        public static Dictionary<string, Texture2D> BackgroundResourcePlay;
        public static Dictionary<string, Texture2D> TextureResource;
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
            BackgroundResourcePlay = Loader.ContentLoad<Texture2D>(content, "Assets\\Backgrounds\\Play");
            TextureResource = Loader.ContentLoad<Texture2D>(content, "Assets\\Sprites");
            AudioResource = Loader.ContentLoad<SoundEffect>(content, "Assets\\Sounds");
            MusicResource = Loader.ContentLoad<Song>(content, "Assets\\Music");
            mainMenuTextures = Loader.ContentLoadQueue<Texture2D>(content, "Assets\\Backgrounds\\MainMenu");
            pointerTextures = Loader.ContentLoadQueue<Texture2D>(content, "Assets\\Sprites\\Select");
            highScoreTextures = Loader.ContentLoadQueue<Texture2D>(content, "Assets\\Backgrounds\\HighScore");

            CreateScenes();
        }

        public override void Update(GameTime gameTime)
        {
            ChangeSceneFromMenu(Listen());
            CheckScore(ScenePlay.Score);

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
            mainMenu = new SceneMenu(mainMenuTextures, pointerTextures, MusicResource["bgm_menu"], Keys.Enter);
            play = new ScenePlay(null, MusicResource["bgm_play"], Keys.Escape);
            play.InitCamera(gameRoot);
            highScore = new SceneHighScore(highScoreTextures, MusicResource["bgm_highScore"], Keys.Escape);

            mainMenu.Active = true;
            Scenes.Push(mainMenu);
        }

        private string Listen()
        {
            return Scenes
                .Where(s => s.GetType() == typeof(SceneMenu))
                .Cast<SceneMenu>()
                .Select(sm => sm.UserInterface)
                .Select(ui => ui.Slots)
                .SelectMany(sl => sl)
                .Where(sl => sl.isClicked == true)
                .Select(sl => sl.Name)
                .SingleOrDefault();
        }

        private void ChangeSceneFromMenu(string sceneName)
        {
            if (sceneName != null)
            {
                switch (sceneName.ToUpper())
                {
                    case "PLAY":
                        SwitchScene(play);
                        break;
                    case "HIGH SCORES":
                        SwitchScene(highScore);
                        break;
                    case "QUIT":
                        Game.Exit();
                        break;
                    default:
                        SwitchScene(mainMenu);
                        break;
                }
            }
        }

        private void SwitchScene(Scene pushScene)
        {
            previousScene = Scenes.Pop();
            previousScene.Active = false;
            MediaPlayer.Stop();
            Scenes.Push(pushScene);
            currentScene = Scenes.Peek();
            currentScene.Active = true;
            MediaPlayer.Play(currentScene.BackingTrack);
        }

        private void CheckScore(int gameScore)
        {
            if (gameScore >= ScenePlay.MAX_SCORE)
            {
                SwitchScene(highScore);
            }
        }
        #endregion
    }
}
