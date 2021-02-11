using System;
using System.Collections.Generic;
using System.Text;

using SFML.System;

namespace EngineSFML.GameObjects.Blocks
{
    public class BlockGrass : Block
    {
        public BlockGrass(Vector2f _pos) : base(_pos, new Vector2f(32, 32), "Resources\\Sprites\\Blocks\\Grass.png", BlockName.grass)
        {

        }

    }
}
