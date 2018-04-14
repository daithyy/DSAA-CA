using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using DSAACA.Entities;
using DSAACA.UI;
using DSAACA.Components;

namespace DSAACA.Backgrounds.Levels
{
    class SceneHighScore : SceneMenu
    {
        #region Properties
        public LinkedList<MenuItem> Scoreboard = new LinkedList<MenuItem>();
        private SpriteFont systemFont;
        #endregion

        #region Constructor
        public SceneHighScore(Queue<Texture2D> textures, Queue<Texture2D> pointerTextures, Song bgm, Keys activateKey, Keys escapeKey)
            : base(textures, pointerTextures, bgm, activateKey, escapeKey)
        {
            Init();
        }
        #endregion

        #region Methods   
        public override void Update(GameTime gameTime)
        {
            UserInterface.Slots = Scoreboard.ToList();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ShowScoreboard(spriteBatch);

            base.Draw(spriteBatch);
        }

        public new void Init()
        {
            systemFont = GameRoot.FontResource["systemFont"];

            FrameSpeed = FrameSpeed / 2;

            int viewportCenterWidth = Helper.GraphicsDevice.Viewport.Width / 2;
            int viewportCenterHeight = Helper.GraphicsDevice.Viewport.Height / 2;

            OrderedInsert(Scoreboard, new MenuItem("Play Again", 0, systemFont, Color.White, new Vector2(viewportCenterWidth, viewportCenterHeight)));
            OrderedInsert(Scoreboard, new MenuItem("Back", 0, systemFont, Color.White, new Vector2(viewportCenterWidth, viewportCenterHeight)));
        }

        public void ShowScoreboard(SpriteBatch spriteBatch)
        {
            // Get the first Score node
            LinkedListNode<MenuItem> first = Scoreboard.First;
            // Measure 
            Vector2 scoreSize = systemFont.MeasureString(first.Value.ToString());
            // Get the center of the viewport
            Vector2 ScorePos = Helper.GraphicsDevice.Viewport.Bounds.Center.ToVector2();
            // Calculate the position of the Scoreboard allowing for the size and number of scores to be displayed
            ScorePos -= new Vector2(scoreSize.X / 2, scoreSize.Y * Scoreboard.Count - Helper.GraphicsDevice.Viewport.Height / 4);

            foreach (var item in Scoreboard)
            {
                item.Position = ScorePos;
                ScorePos += new Vector2(0, systemFont.MeasureString(item.ToString()).Y + 10);
            }
        }

        public void OrderedInsert(LinkedList<MenuItem> list, MenuItem newScore)
        {
            LinkedListNode<MenuItem> node = list.First;
            while (node != null && node.Value.Score >= newScore.Score)
            {
                node = node.Next;
            }
            if (node == null && list.First == null)
                list.AddFirst(newScore);
            else if (node == null)
            {
                list.AddAfter(list.Last, newScore);
            }
            else list.AddBefore(node, newScore);
        }
        #endregion
    }
}
