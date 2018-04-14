using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace DSAACA.Entities
{
    public class Sprite
    {
        #region Properties
        // Initialize public variables ...
        public bool IsActive;
        public Texture2D Image;
        public Vector2 Position;
        private Rectangle bounds;
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Position.ToPoint(), bounds.Size);
            }
            set
            {
                bounds = value;
            }
        }
        public Color Tint;

        // Animated Sprite
        // Source Rectangle used to determine where inside the spritesheet we are drawing ...
        public Rectangle SourceRectangle;

        public Vector2 CentrePosition
        {
            get
            {
                return new Vector2(Position.X + Bounds.Width / 2, Position.Y + Bounds.Height / 2);
            }
        }

        int currentFrame = 0;
        int numberOfFrames = 0;
        int millisecondsBetweenFrames = 100;
        float elapsedTime = 0;
        #endregion

        #region Constructor
        public Sprite(Texture2D image, Vector2 position, int frameCount)
        {
            // We set the number of frames of the sprite to the frame count for the spritesheet ...
            numberOfFrames = frameCount;

            // Assigning passed in variables to our object's variables ...
            Image = image;
            Position = position;
            Tint = Color.White;

            // Animated Sprite
            // Width is now total width / number of frames ...
            Bounds = new Rectangle((int)position.X, (int)position.Y,
                image.Width / frameCount,
                image.Height);

            IsActive = true;
        }
        #endregion

        #region Methods
        // Update Method
        public void UpdateAnimation(GameTime gameTime)
        {
            if (IsActive)
            {
                // Track how much time has passed ...
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                // If it's greater than the frame time then move to the next frame ...
                if (elapsedTime >= millisecondsBetweenFrames)
                {
                    currentFrame++;

                    if (currentFrame > (numberOfFrames - 1))
                        currentFrame = 0;

                    elapsedTime = 0;
                }

                // Update our source rectangle ...
                SourceRectangle = new Rectangle(
                    currentFrame * (Image.Width / numberOfFrames), // This bracket can be a variable (spriteWidth)
                    0,
                    Image.Width / numberOfFrames,
                    Image.Height);
            }
        }

        // Draw Method (Caller has a spritebatch ready and has already called SpriteBatch.Begin())
        public void Draw(SpriteBatch sp)
        {
            if (IsActive)
                sp.Draw(Image, Position, SourceRectangle, Tint);
        }

        // Overload Draw Method (Same name, different arguments)
        public void Draw(SpriteBatch sp, SpriteFont sfont)
        {
            if (IsActive)
            {
                Draw(sp); // Call the Draw Method above ...
                sp.DrawString(sfont, Position.ToString(), Position, Color.White);
            }
        }

        // Move Method (Move the sprite by a given amount)
        public virtual void Move(Vector2 delta)
        {
            Position += delta;
        }

        // Collision Method ()
        public bool CheckCollision(Sprite other)
        {
            if (Bounds.Intersects(other.Bounds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}