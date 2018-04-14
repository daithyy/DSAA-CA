using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DSAACA.Backgrounds.Levels;
using DSAACA.Backgrounds;
using Microsoft.Xna.Framework.Audio;

namespace DSAACA.Entities
{
    class Collectable : Sprite
    {
        #region Properties        
        public const int SCORE_AMOUNT = 8;
        private SoundEffectInstance sndCollect;
        #endregion

        #region Constructor
        public Collectable(Texture2D image, Vector2 position, int frameCount) : base(image, position, frameCount)
        {
            sndCollect = SceneManager.AudioResource["snd_collect"].CreateInstance();
        }
        #endregion

        #region Methods
        public void Update(GameTime gameTime, Player player)
        {
            if (IsActive)
            {            
                if (CheckCollision(player))
                {
                    ScenePlay.Score += SCORE_AMOUNT;
                    IsActive = false;
                    if (sndCollect.State != SoundState.Playing)
                        sndCollect.Play();
                }
            }
        }
        #endregion
    }
}
