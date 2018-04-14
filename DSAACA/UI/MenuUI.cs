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
using DSAACA.Backgrounds;
using DSAACA.Components;

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
                return new Rectangle(
                    (Position.ToPoint() - Texture.Bounds.Center)
                    + Camera.CamPos.ToPoint(), 
                    Texture.Bounds.Size * new Point(4));
            }
        }
        public Queue<Texture2D> Pointer { get; set; }
        private Vector2 StartPosition { get; set; }
        public Vector2 Position { get; set; }
        public bool isVisible = true;
        public List<MenuItem> Slots;
        public int SlotPosition = 0;
        private TimeSpan frameTime;
        private const float CURSOR_SPEED = 0.25f;
        private const float FRAME_SPEED = 25;
        private const float SPACING = 100;
        private Keys ActivateKey;
        #endregion

        #region Constructor
        public MenuUI(Queue<Texture2D> textures, List<MenuItem> slots, Keys activationKey)
        {
            Pointer = textures;
            Slots = slots;
            ActivateKey = activationKey;

            Texture = Pointer.Dequeue();
            Pointer.Enqueue(Texture);
        }
        #endregion

        #region Methods
        public virtual void Update(GameTime gameTime)
        {
            UpdateSlots(gameTime);
            UpdateSelectPosition();
            HandleSelection();
            UpdateAnimation(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(Texture, BoundingRectangle, Color.White);

                foreach (MenuItem item in Slots)
                {
                    item.Draw(spriteBatch);
                }
            }
        }

        private void UpdateSelectPosition()
        {
            Position = Vector2.Lerp(Position, new Vector2(
                Slots[SlotPosition].Position.X - SPACING, 
                Slots[SlotPosition].Position.Y), CURSOR_SPEED);

            if (InputEngine.IsKeyPressed(Keys.S) ||
                InputEngine.IsKeyPressed(Keys.Down)
                && SlotPosition <= (Slots.Count - 1))
            {
                SceneManager.AudioResource["snd_cursor"].Play();
                SlotPosition++;
                if (SlotPosition == (Slots.Count))
                {                    
                    SlotPosition = 0;
                }
            }
            else if (InputEngine.IsKeyPressed(Keys.W) ||
                InputEngine.IsKeyPressed(Keys.Up)
                && SlotPosition >= 0)
            {
                SceneManager.AudioResource["snd_cursor"].Play();
                if (SlotPosition == 0)
                {
                    SlotPosition = Slots.Count;
                }
                SlotPosition--;
            }
        }

        private void HandleSelection()
        {
            if (InputEngine.IsKeyPressed(ActivateKey))
            {
                Slots[SlotPosition].isClicked = true;
            }
        }

        private void UpdateSlots(GameTime gameTime)
        {
            foreach (MenuItem item in Slots)
            {
                item.Update(gameTime);
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

        public void ResetSlotState()
        {
            foreach (MenuItem item in Slots)
            {
                item.isClicked = false;
            }
        }
        #endregion
    }
}
