using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DSAACA.Entities
{
    class Collectables : Sprite
    {
        public Collectables(Texture2D image, Vector2 position, int frameCount) : base(image, position, frameCount)
        {

        }
    }
}
