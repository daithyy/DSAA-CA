using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InputManager;

namespace DataStructureGame
{
    class GUI
    {
        public Texture2D Pointer { get; set; }
        private Vector2 StartPosition { get; set; }
        public Vector2 Position { get; set; }
        public bool isVisible = true;
        private Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Pointer.Width, Pointer.Height); }
        }
        private ColorObject[] colorObjects;
        public int SlotPosition = 0;

        public GUI(Texture2D textureIn, Vector2 positionIn, ColorObject[] colorObjectsIn)
        {
            Pointer = textureIn;
            Position = positionIn;
            StartPosition = positionIn;
            colorObjects = colorObjectsIn;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.Position = new Vector2(
                colorObjects[SlotPosition].Position.X
                + (colorObjects[SlotPosition].Texture.Width / 4),
                colorObjects[SlotPosition].Position.Y 
                + (colorObjects[SlotPosition].Texture.Height 
                + (colorObjects[SlotPosition].Texture.Height / 4)));

            if (InputEngine.IsKeyPressed(Keys.Right) && SlotPosition <= (ColorObject.Count - 1))
            {
                SlotPosition++;
                if (SlotPosition == (ColorObject.Count))
                {
                    this.Position = StartPosition;
                    SlotPosition = 0;
                }
            }
            else if (InputEngine.IsKeyPressed(Keys.Left) && SlotPosition >= 0)
            {
                if (SlotPosition == 0)
                {
                    SlotPosition = ColorObject.Count;
                }
                SlotPosition--;
            }

            HandleSelection();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(Pointer, Bounds, Color.White);
            }
        }

        private void HandleSelection()
        {
            if (InputEngine.IsKeyPressed(Keys.Enter))
            {
                colorObjects[SlotPosition].isClicked = true;
            }
        }
    }
}
