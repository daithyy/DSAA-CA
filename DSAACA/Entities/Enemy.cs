using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DSAACA.Entities
{
    class Enemy : Player
    {
        #region Properties
        private Vector2 destination;
        public bool CanMove;
        #endregion

        #region Constructor
        public Enemy(Texture2D image, Vector2 position, Vector2 destination, int frameCount)
            : base(image, position, frameCount)
        {
            this.destination = destination;
            CanMove = false;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
            base.Update(gameTime);
        }

        public override void HandleMovement()
        {
            if (destination != null 
                && Position != destination
                && CanMove)
            {
                Velocity += Acceleration;
            }
            else
            {
                Velocity -= Deceleration;
            }
        }
        #endregion
    }
}
