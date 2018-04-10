using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace DSAACA.Backgrounds.Levels
{
    class SceneHighScore : Scene
    {
        #region Properties

        #endregion

        #region Constructor
        public SceneHighScore(Vector2 position, Texture2D texture, Song bgm, Keys key)
            : base(position, texture, bgm, key)
        {

        }
        #endregion

        #region Methods
        public override void Draw(SpriteBatch sp)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
