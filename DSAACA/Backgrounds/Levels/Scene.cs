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
        private Sprite _tx;
        public bool Active { get; set; }
        public Sprite Texture
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
        public SoundEffectInstance SoundPlayer { get; set; }
        public Keys ActivationKey;
        #endregion

        #region Constructor
        public Scene(Sprite tx, Song bgm, Keys key)
        {
            _tx = tx;
            BackingTrack = bgm;
            ActivationKey = key;
        }
        #endregion

        #region Methods
        public abstract void Update();

        public abstract void Draw(SpriteBatch sp);
        #endregion
    }
}
