using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DSAACA.Entities
{
    class StartTower : Sprite
    {
        #region Properties
        private Queue<Enemy> enemies;
        private EndTower destinationTower;
        private const int TOWER_RADIUS = 400;
        #endregion

        #region Constructor
        public StartTower(Texture2D image, Vector2 position, int frameCount)
            : base(image, position, frameCount)
        {

        }
        #endregion

        #region Methods

        #endregion
    }
}
