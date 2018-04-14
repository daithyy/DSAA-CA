using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DSAACA.Entities;
using DSAACA.Components;

namespace DSAACA.UI
{
    class MenuItem
    {
        #region Properties
        public static int Count = 0;
        private SpriteFont UIFont { get; set; }
        public Color CurrentColor { get; set; }
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool isClicked = false;
        public bool isVisible = true;
        #endregion

        #region Constructor
        public MenuItem(string nameIn, int scoreIn, SpriteFont UIFontIn, Color colorIn, Vector2 positionIn)
        {
            Count++;
            Name = nameIn;
            Score = scoreIn;
            CurrentColor = colorIn;
            Position = positionIn;
            UIFont = UIFontIn;
        }
        #endregion

        #region Methods
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                if (Score == 0)
                    spriteBatch.DrawString(UIFont, Name, Position + Camera.CamPos, CurrentColor);
                else
                    spriteBatch.DrawString(UIFont, ToString(), Position + Camera.CamPos, CurrentColor);
            }
        }

        public override string ToString()
        {
            return "Name: " + Name + " Score: " + Score.ToString();
        }
        #endregion
    }
}
