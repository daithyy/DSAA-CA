using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DSAACA.Entities;

namespace DSAACA.UI
{
    class MenuItem
    {
        #region Properties
        public static int Count = 0;
        private SpriteFont UIFont { get; set; }
        public Sprite Texture { get; set; }
        public Color CurrentColor { get; set; }
        public Vector2 Position { get; set; }
        private Rectangle BoundingRectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Bounds.Width, Texture.Bounds.Height); }
        }
        public string Name { get; set; }
        public bool isClicked = false;
        public bool isVisible = true;
        #endregion

        #region Constructor
        public MenuItem(string nameIn, Sprite textureIn, SpriteFont UIFontIn, Color colorIn, Vector2 positionIn)
        {
            Count++;
            Name = nameIn;
            Texture = textureIn;
            CurrentColor = colorIn;
            Position = positionIn;
            UIFont = UIFontIn;
        }
        #endregion

        #region Methods
        public virtual void Update(GameTime gameTime)
        {
            if (Intersect() && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                this.isClicked = true;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(Texture.Image, Texture.Bounds, CurrentColor);
                spriteBatch.DrawString(UIFont, Name, 
                    new Vector2(this.Position.X + Texture.Bounds.Width / 5, 
                    this.Position.Y - Texture.Bounds.Height), Color.White);
            }
        }

        public bool Intersect()
        {
            if (Mouse.GetState().X < BoundingRectangle.X + Texture.Bounds.Width &&
                Mouse.GetState().X > BoundingRectangle.X &&
                Mouse.GetState().Y < BoundingRectangle.Y + Texture.Bounds.Height &&
                Mouse.GetState().Y > BoundingRectangle.Y)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
