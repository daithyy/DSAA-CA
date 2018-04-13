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
        private Queue<Texture2D> _textures;
        public bool Active { get; set; }
        public Queue<Texture2D> Textures
        {
            get
            {
                return _textures;
            }
            set
            {
                _textures = value;
            }
        }
        public Song BackingTrack { get; set; }
        public Vector2 Position { get; set; }
        public Keys ActivationKey;
        public float Alpha;
        #endregion

        #region Constructor
        public Scene(Queue<Texture2D> textures, Song bgm, Keys key)
        {
            Textures = textures;
            BackingTrack = bgm;
            ActivationKey = key;
            Active = false;
        }
        #endregion

        #region Methods
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion
    }
}
