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
        private StartTower start;
        private EndTower destination;
        private Vector2 direction;
        public bool CanMove;
        #endregion

        #region Constructor
        public Enemy(Texture2D image, Vector2 position, StartTower start, EndTower destination, int frameCount)
            : base(image, position, frameCount)
        {
            this.start = start;
            this.destination = destination;
            CanMove = false;

            direction = destination.Position - position;
            direction.Normalize();
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
                && Position != destination.Position
                && CanMove)
            {
                Position += direction;
            }
            else
            {
                //if (Velocity != Vector2.Zero)
                //    Velocity -= Deceleration;
                CanMove = false;
            }
        }

        public bool RequeueCheck()
        {
            if (CheckCollision(destination))
            {
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}
