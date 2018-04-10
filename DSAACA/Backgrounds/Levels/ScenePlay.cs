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

        #endregion

        #region Constructor
        public ScenePlay(Vector2 position, Sprite texture, Song bgm, Keys key)
            : base(position, texture, bgm, key)
        {

        }
        #endregion

        #region Methods
        public override void Update()
        {

        }

        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(Texture.Image, new Rectangle(Position.ToPoint(), new Point(
            Helper.GraphicsDevice.Viewport.Bounds.Width,
            Helper.GraphicsDevice.Viewport.Bounds.Height)), Color.White);
        }
        #endregion
    }
}
