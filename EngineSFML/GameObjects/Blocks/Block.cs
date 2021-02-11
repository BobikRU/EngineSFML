using System;
using System.Collections.Generic;
using System.Text;

using EngineSFML.Main;

using SFML.System;

namespace EngineSFML.GameObjects.Blocks
{
    public abstract class Block : GameObject
    {

        public const int BlockWidth = 32;
        public const int BlockHeight = 32;

        public const int BlockNameCount = 1;
        public enum BlockName
        {
            grass
        }

        public enum TypeBlock
        {
            solid,
            liquid
        }

        protected TypeBlock blockType;
        public TypeBlock BlockType { get { return blockType; } }

        protected Block(Vector2f _pos, Vector2f _size, string _filename, BlockName _name, TypeBlock _blocktype = TypeBlock.solid) : base (_pos, _size, _filename, Type.block, _name.ToString())
        {
            blockType = _blocktype;

            Pos = new Vector2f(BlockWidth * ((int)PosX / BlockWidth) + BlockWidth / 2, BlockHeight * ((int)PosY / BlockHeight) + BlockHeight / 2);
            Sprite.Scale = new Vector2f((float)BlockWidth / (float)Sprite.TextureRect.Width, (float)BlockHeight / (float)Sprite.TextureRect.Height);
        }

        public override void Update()
        {
            Pos = new Vector2f(BlockWidth * ((int)PosX / BlockWidth) + BlockWidth /2, BlockHeight * ((int)PosY / BlockHeight) + BlockHeight / 2);
            Sprite.Scale = new Vector2f((float)BlockWidth / (float)Sprite.TextureRect.Width, (float)BlockHeight / (float)Sprite.TextureRect.Height);
            base.Update();
        }

    }
}
