using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using DSAACA.Components;
using DSAACA.Entities;
using System.Threading;

namespace DSAACA.Backgrounds.Levels
{
    class ScenePlay : Scene
    {
        #region Properties
        public static int Count;
        private SpriteFont systemFont;
        private Texture2D backgroundTexture;
        public static Vector2 WorldBounds
        {
            get
            {
                return new Vector2(1920, 1080);
            }
        }
        private Camera currentCamera;
        public const int MAX_SCORE = COLLECTABLE_AMOUNT * Collectable.SCORE_AMOUNT;
        public static int Score;

        // Entities
        public Player player;
        private const int TOWER_AMOUNT = 5;
        private List<StartTower> towers;
        private const int COLLECTABLE_AMOUNT = 9;
        private List<Collectable> collectables;
        private List<Enemy> activeEnemies;
        #endregion

        #region Constructor
        public ScenePlay(Queue<Texture2D> textures, Song bgm, Keys activateKey, Keys escapeKey)
            : base(textures, bgm, activateKey, escapeKey)
        {
            Init();
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (currentCamera != null)
                Camera.Follow(player.CentrePosition, Helper.GraphicsDevice.Viewport, currentCamera.CameraSpeed);

            player.Update(gameTime);
            player.UpdateAnimation(gameTime);
            ClampPlayer(WorldBounds);

            foreach (Collectable item in collectables)
            {
                item.UpdateAnimation(gameTime);
                item.Update(gameTime, player);
            }

            foreach (StartTower tower in towers)
            {
                tower.Update(gameTime, activeEnemies);

                if (ObjectWithinViewport(tower, Helper.GraphicsDevice.Viewport)
                    && ObjectWithinViewport(tower.DestinationTower, Helper.GraphicsDevice.Viewport))
                {
                    tower.StartQueue(gameTime, activeEnemies);
                }
            }

            foreach (Enemy enemy in activeEnemies)
            {
                enemy.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(new Point(0), WorldBounds.ToPoint()), Color.White);
            player.Draw(spriteBatch);

            foreach (Collectable item in collectables)
            {
                item.Draw(spriteBatch);
            }

            foreach (StartTower item in towers)
            {
                if (ObjectWithinViewport(item, Helper.GraphicsDevice.Viewport))
                {
                    item.Draw(spriteBatch);
                }

                if (ObjectWithinViewport(item.DestinationTower, Helper.GraphicsDevice.Viewport))
                {
                    item.DestinationTower.Draw(spriteBatch);
                }
            }

            foreach (Enemy enemy in activeEnemies)
            {
                if (ObjectWithinViewport(enemy, Helper.GraphicsDevice.Viewport))
                {
                    enemy.Draw(spriteBatch);
                }
            }

            spriteBatch.DrawString(systemFont, "SCORE: " + Score,
                new Vector2((Helper.GraphicsDevice.Viewport.Width / 2) -
                (systemFont.MeasureString("SCORE: " + Score.ToString()).X / 2), 32) + Camera.CamPos, Color.White);
        }

        private void Init()
        {
            Interlocked.Increment(ref Count);
            systemFont = GameRoot.FontResource["systemFont"];
            backgroundTexture = SceneManager.BackgroundResourcePlay["bg_ground"];
            player = new Player(SceneManager.TextureResource["player"], new Vector2(100, 100), 1);
            collectables = new List<Collectable>();
            towers = new List<StartTower>();
            activeEnemies = new List<Enemy>();

            for (int i = 0; i < COLLECTABLE_AMOUNT; i++)
            {
                collectables.Add(CreateCollectable());
            }

            for (int i = 0; i < TOWER_AMOUNT; i++)
            {
                towers.Add(CreateTower());
            }
        }

        public void InitCamera(Game game)
        {
            currentCamera = new Camera(game, Vector2.Zero, WorldBounds);
        }

        private Collectable CreateCollectable()
        {
            // Getting random x and y positions ...
            int xPosition = Camera.Random.Next(100, Helper.GraphicsDevice.Viewport.Bounds.Width - 100);
            int yPosition = Camera.Random.Next(100, Helper.GraphicsDevice.Viewport.Bounds.Height - 100);

            return new Collectable(SceneManager.TextureResource["coin"], new Vector2(xPosition, yPosition), 10);
        }

        private StartTower CreateTower()
        {
            Texture2D texture = SceneManager.TextureResource["towerStart"];

            int xPosition = Camera.Random.Next(texture.Width, (int)WorldBounds.X - texture.Width);
            int yPosition = Camera.Random.Next(texture.Height, (int)WorldBounds.Y - texture.Height);

            return new StartTower(texture, new Vector2(xPosition, yPosition), 1);
        }

        private void ClampPlayer(Vector2 WorldBounds)
        {
            player.Position = Vector2.Clamp(
                player.Position, 
                Vector2.Zero, 
                new Vector2(WorldBounds.X - player.Bounds.Width, WorldBounds.Y - player.Bounds.Height));
        }

        public static bool ObjectWithinViewport(Sprite item, Viewport viewport)
        {
            if (item.CentrePosition.X < (viewport.Width + Camera.CamPos.X) 
                && item.CentrePosition.Y < (viewport.Height + Camera.CamPos.Y))
                return true;
            else
                return false;
        }
        #endregion
    }
}
