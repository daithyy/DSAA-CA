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
        public Texture2D Image;
        public Vector2 Position;
        public Rectangle Bounds;
        public Color Tint;

        // Animated Sprite
        // Source Rectangle used to determine where inside the spritesheet we are drawing ...
        public Rectangle sourceRectangle;

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
        }
        #endregion

        #region Methods
        // Update Method
        public void UpdateAnimation(GameTime gameTime)
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
            sourceRectangle = new Rectangle(
                currentFrame * (Image.Width / numberOfFrames), // This bracket can be a variable (spriteWidth)
                0,
                Image.Width / numberOfFrames,
                Image.Height);
        }

        // Draw Method (Caller has a spritebatch ready and has already called SpriteBatch.Begin())
        public void Draw(SpriteBatch sp)
        {
            sp.Draw(Image, Position, sourceRectangle, Tint);
        }

        // Overload Draw Method (Same name, different arguments)
        public void Draw(SpriteBatch sp, SpriteFont sfont)
        {
            Draw(sp); // Call the Draw Method above ...
            sp.DrawString(sfont, Position.ToString(), Position, Color.White);
        }

        // Move Method (Move the sprite by a given amount)
        public void Move(Vector2 delta)
        {
            Position += delta;
            Bounds.X = (int)Position.X;
            Bounds.Y = (int)Position.Y;
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