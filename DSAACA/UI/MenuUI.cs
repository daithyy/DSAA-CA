using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InputManager;
using DSAACA.Entities;

namespace DSAACA.UI
{
    class MenuUI
    {
        #region Properties
        private Texture2D Texture { get; set; }
        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(Position.ToPoint(), 
                    Texture.Bounds.Size * new Point(4));
            }
        }
        public Queue<Texture2D> Pointer { get; set; }
        private Vector2 StartPosition { get; set; }
        public Vector2 Position { get; set; }
        public bool isVisible = true;
        private List<MenuItem> Slots;
        public int SlotPosition = 0;
        private TimeSpan frameTime;
        private const float FRAME_SPEED = 25;
        private Keys ActivateKey;
        #endregion

        #region Constructor
        public MenuUI(Queue<Texture2D> textures, Vector2 position, List<MenuItem> slots, Keys activationKey)
        {
            Pointer = textures;
            Position = position;
            StartPosition = position;
            Slots = slots;
            ActivateKey = activationKey;

            Texture = Pointer.Dequeue();
            Pointer.Enqueue(Texture);
        }
        #endregion

        #region Methods
        public virtual void Update(GameTime gameTime)
        {
            Position = new Vector2(Slots[SlotPosition].Position.X, Slots[SlotPosition].Position.Y);

            if (InputEngine.IsKeyPressed(Keys.Right) && SlotPosition <= (MenuItem.Count - 1))
            {
                SlotPosition++;
                if (SlotPosition == (MenuItem.Count))
                {
                    this.Position = StartPosition;
                    SlotPosition = 0;
                }
            }
            else if (InputEngine.IsKeyPressed(Keys.Left) && SlotPosition >= 0)
            {
                if (SlotPosition == 0)
                {
                    SlotPosition = MenuItem.Count;
                }
                SlotPosition--;
            }

            HandleSelection();
            UpdateAnimation(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(Texture, BoundingRectangle, Color.White);
            }
        }

        private void HandleSelection()
        {
            if (InputEngine.IsKeyPressed(ActivateKey))
            {
                Slots[SlotPosition].isClicked = true;
            }
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            // Track how much time has passed ...
            frameTime += gameTime.ElapsedGameTime;

            // If it's greater than the frame time then move to the next frame ...
            if (frameTime.Milliseconds >= FRAME_SPEED)
            {
                Texture = Pointer.Dequeue();
                Pointer.Enqueue(Texture);
                frameTime = TimeSpan.Zero;
            }
        }
        #endregion
    }
}
