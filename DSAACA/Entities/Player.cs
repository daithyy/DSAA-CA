using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InputManager;
using Microsoft.Xna.Framework.Input;

namespace DSAACA.Entities
{
    class Player : Sprite
    {
        #region Properties
        private Vector2 Velocity;
        private Vector2 Acceleration;
        private Vector2 Deceleration;
        private Vector2 MaxVelocity;
        public Vector2 PreviousPosition;
        #endregion

        #region Constructor
        public Player(Texture2D image, Vector2 position, int frameCount) 
            : base(image, position, frameCount)
        {
            Init();
        }
        #endregion

        #region Methods
        public void Update(GameTime gameTime)
        {
            PreviousPosition = Position;
            HandleMovement();
            Velocity = Vector2.Clamp(Velocity, -MaxVelocity, MaxVelocity);
            Position += Velocity;
        }

        private void Init()
        {
            const float MAX_VELOCITY = 3f;
            const float DECCELERATION = 0.2f;
            const float ACCELERATION = 0.5f;

            MaxVelocity = new Vector2(MAX_VELOCITY);
            Acceleration = new Vector2(ACCELERATION);
            Deceleration = new Vector2(DECCELERATION);
        }

        public void HandleMovement()
        {
            switch (InputEngine.UsingKeyboard)
            {
                case true:
                    #region Handle Keyboard Movement
                    if (InputEngine.IsKeyHeld(Keys.W))
                    {
                        Velocity.Y -= Acceleration.Y;
                    }
                    else if (Velocity.Y < 0)
                    {
                        Velocity.Y += Deceleration.Y;
                    }
                    else if (InputEngine.IsKeyHeld(Keys.S))
                    {
                        Velocity.Y += Acceleration.Y;
                    }
                    else if (Velocity.Y > 0)
                    {
                        Velocity.Y -= Deceleration.Y;
                    }

                    if (InputEngine.IsKeyHeld(Keys.A))
                    {
                        Velocity.X -= Acceleration.X;
                    }
                    else if (Velocity.X < 0)
                    {
                        Velocity.X += Deceleration.X;
                    }
                    else if (InputEngine.IsKeyHeld(Keys.D))
                    {
                        Velocity.X += Acceleration.X;
                    }
                    else if (Velocity.X > 0)
                    {
                        Velocity.X -= Deceleration.X;
                    }
                    #endregion
                    break;
                case false:
                    #region Handle Controller Movement
                    Velocity += InputEngine.CurrentPadState.ThumbSticks.Left * Acceleration;
                    #endregion
                    break;
            }
        }
        #endregion
    }
}
