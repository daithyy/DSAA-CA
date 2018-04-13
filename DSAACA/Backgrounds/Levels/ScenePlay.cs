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

namespace DSAACA.Backgrounds.Levels
{
    class ScenePlay : Scene
    {
        #region Properties
        private Vector2 playAreaSize
        {
            get
            {
                return new Vector2(1920, 1080);
            }
        }
        private Camera currentCamera;
        public Player player;
        #endregion

        #region Constructor
        public ScenePlay(Queue<Texture2D> textures, Song bgm, Keys key)
            : base(textures, bgm, key)
        {
            Init();
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        private void Init()
        {
            player = new Player()
        }

        public void InitCamera(Game game)
        {
            currentCamera = new Camera(game, player.Position, playAreaSize);
        }
        #endregion
    }
}
