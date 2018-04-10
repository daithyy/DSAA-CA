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
        public Sprite Pointer { get; set; }
        private Vector2 StartPosition { get; set; }
        public Vector2 Position { get; set; }
        public bool isVisible = true;
        private List<MenuItem> Slots;
        public int SlotPosition = 0;

        public MenuUI(Sprite textureIn, Vector2 positionIn, List<MenuItem> slotsIn)
        {
            Pointer = textureIn;
            Position = positionIn;
            StartPosition = positionIn;
            Slots = slotsIn;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.Position = new Vector2(
                Slots[SlotPosition].Position.X
                + (Slots[SlotPosition].Texture.Bounds.Width / 4),
                Slots[SlotPosition].Position.Y 
                + (Slots[SlotPosition].Texture.Bounds.Height 
                + (Slots[SlotPosition].Texture.Bounds.Height / 4)));

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
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(Pointer.Image, Pointer.Bounds, Color.White);
            }
        }

        private void HandleSelection()
        {
            if (InputEngine.IsKeyPressed(Keys.Enter))
            {
                Slots[SlotPosition].isClicked = true;
            }
        }
    }
}
