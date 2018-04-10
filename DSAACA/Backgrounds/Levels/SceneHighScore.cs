using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using DSAACA.Entities;

namespace DSAACA.Backgrounds.Levels
{
    class SceneHighScore : Scene
    {
        #region Properties

        #endregion

        #region Constructor
        public SceneHighScore(Sprite texture, Song bgm, Keys key)
            : base(texture, bgm, key)
        {

        }
        #endregion

        #region Methods
        public override void Draw(Game game)
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
