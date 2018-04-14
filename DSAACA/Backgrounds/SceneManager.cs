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

            currentScene = Scenes.Peek();
            if (currentScene.Active)
                currentScene.Update(gameTime);

            ListenExit();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)gameRoot.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Begin(
                SpriteSortMode.Immediate, 
                BlendState.AlphaBlend, 
                SamplerState.PointClamp, null, null, null, Camera.CurrentCameraTranslation);

            currentScene = Scenes.Peek();
            if (currentScene.Active)
                currentScene.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CreateScenes()
        {
            mainMenu = new SceneMenu(mainMenuTextures, pointerTextures, MusicResource["bgm_menu"], Keys.Enter, Keys.None);
            play = new ScenePlay(null, MusicResource["bgm_play"], Keys.None, Keys.Escape);
            play.InitCamera(gameRoot);
            highScore = new SceneHighScore(
                highScoreTextures, pointerTextures, MusicResource["bgm_highScore"], Keys.None, Keys.Escape);

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

                mainMenu.UserInterface.ResetSlotState();
            }
        }

        private void SwitchScene(Scene pushScene)
        {
            if (pushScene != null)
            {
                previousScene = Scenes.Pop();
                previousScene.Active = false;
                MediaPlayer.Stop();
                Scenes.Push(pushScene);
                currentScene = Scenes.Peek();
                currentScene.Active = true;

                AudioResource["snd_cursorE"].Play();            
                MediaPlayer.Play(currentScene.BackingTrack);
            }
        }

        private void CheckScore(int gameScore)
        {
            if (gameScore >= ScenePlay.MAX_SCORE)
            {
                SwitchScene(highScore);
            }
        }

        private void ListenExit()
        {
            if (currentScene.EscapeScene())
                SwitchScene(previousScene);
        }
        #endregion
    }
}
