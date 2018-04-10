using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using DSAACA.Components;
using DSAACA.Entities;

namespace DSAACA.Backgrounds
{
    public abstract class Scene
    {
        #region Properties
        private Texture2D _tx;
        public bool Active { get; set; }
        public Texture2D Texture
        {
            get
            {
                return _tx;
            }
            set
            {
                _tx = value;
            }
        }
        public Song BackingTrack { get; set; }
        public Vector2 Position { get; set; }
        public Keys ActivationKey;
        #endregion

        #region Constructor
        public Scene(Texture2D tx, Song bgm, Keys key)
        {
            _tx = tx;
            BackingTrack = bgm;
            ActivationKey = key;
            Active = false;
        }
        #endregion

        #region Methods
        public abstract void Update();

        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion
    }
}
