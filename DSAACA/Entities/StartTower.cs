using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DSAACA.Backgrounds;
using DSAACA.Backgrounds.Levels;
using DSAACA.Components;

namespace DSAACA.Entities
{
    class StartTower : Sprite
    {
        #region Properties
        public Queue<Enemy> Enemies;
        public EndTower DestinationTower;
        private TimeSpan DequeueInterval;
        private const int ENEMY_COUNT = 3;
        private const double TOWER_RADIUS = 400;
        #endregion

        #region Constructor
        public StartTower(Texture2D image, Vector2 position, int frameCount)
            : base(image, position, frameCount)
        {
            Init();
        }
        #endregion

        #region Methods
        public void Update(GameTime gameTime, List<Enemy> activeEnemies)
        {
            if (ScenePlay.ObjectWithinViewport(DestinationTower, Helper.GraphicsDevice.Viewport))
            {
                DestinationTower.UpdateAnimation(gameTime);
            }

            foreach (Enemy enemy in activeEnemies)
            {
                if (enemy.RequeueCheck())
                {
                    Enemies.Enqueue(CreateEnemy());
                }
            }

            activeEnemies.RemoveAll(enemy => enemy.RequeueCheck() == true);

            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime);
            }

            UpdateAnimation(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        
        private void Init()
        {
            DestinationTower = CreateEndTower();
            Enemies = new Queue<Enemy>();

            for (int i = 0; i < ENEMY_COUNT; i++)
            {
                Enemies.Enqueue(CreateEnemy());
            }
        }

        private EndTower CreateEndTower()
        {
            return new EndTower(SceneManager.TextureResource["towerEnd"], CalculateEndTowerPosition(TOWER_RADIUS), 1);
        }

        private Enemy CreateEnemy()
        {
            return new Enemy(SceneManager.TextureResource["blackKnight"], CentrePosition, this, DestinationTower, 1);
        }

        private Vector2 CalculateEndTowerPosition(double radius)
        {
            var angle = Camera.Random.NextDouble() * Math.PI * 2;
            radius = Math.Sqrt(Camera.Random.NextDouble()) * radius;
            var x = CentrePosition.X + radius * Math.Cos(angle);
            var y = CentrePosition.Y + radius * Math.Sin(angle);

            if (x > ScenePlay.WorldBounds.X - Image.Width)
                x += ((ScenePlay.WorldBounds.X - x) - Image.Width);

            if (y > ScenePlay.WorldBounds.Y - Image.Width)
                y += ((ScenePlay.WorldBounds.Y - y) - Image.Height);

               return new Vector2((float)x, (float)y);
        }

        public void StartQueue(GameTime gameTime, List<Enemy> activeList)
        {
            DequeueInterval += gameTime.ElapsedGameTime;

            if (DequeueInterval.Seconds >= 1 && Enemies.Count >= 1)
            {
                Enemy enemy = Enemies.Dequeue();
                activeList.Add(enemy);
                enemy.CanMove = true;
                DequeueInterval = TimeSpan.Zero;
            }
        }

        public void Requeue(Enemy enemy)
        {
            Enemies.Enqueue(enemy);
        }
        #endregion
    }
}
