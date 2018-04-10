using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DSAACA.Backgrounds.Levels
{
    class SceneMenu : Scene
    {
        #region Properties

        #endregion

        #region Constructor
        public SceneMenu(Vector2 position, Texture2D texture, Song bgm, Keys key)
            : base(position, texture, bgm, key)
        {

        }

        #endregion

        #region Methods
        public override void Draw(SpriteBatch sp)
        {

        }

        public override void Update()
        {
            
        }
        #endregion
    }
}
