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
        private Texture2D texture;
        private Queue<Texture2D> _textures;
        public bool Active { get; set; }
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }
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
        private TimeSpan frameTime;
        public const float FRAME_SPEED = 100;
        #endregion

        #region Constructor
        public Scene(Queue<Texture2D> textures, Song bgm, Keys key)
        {
            Textures = textures;
            BackingTrack = bgm;
            ActivationKey = key;
            Active = false;
            Alpha = 1.0f;

            if (textures != null)
            {
                texture = textures.Dequeue();
                textures.Enqueue(texture);
            }
        }
        #endregion

        #region Methods
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public void UpdateAnimation(GameTime gameTime)
        {
            // Track how much time has passed ...
            frameTime += gameTime.ElapsedGameTime;

            // If it's greater than the frame time then move to the next frame ...
            if (frameTime.Milliseconds >= FRAME_SPEED)
            {
                Texture = Textures.Dequeue();
                Textures.Enqueue(Texture);
                frameTime = TimeSpan.Zero;
            }
        }
        #endregion
    }
}
