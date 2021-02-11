
using SFML.Graphics;
using SFML.System;

namespace EngineSFML.GameObjects
{
    public class HitBox
    {
        private FloatRect rect;
        public FloatRect Rect { get { return rect; } }

        public float Width { get { return rect.Width; } set { rect.Width = value; } }
        public float Height { get { return rect.Height; } set { rect.Height = value; } }
        public float PosX { get { return rect.Left; } set { rect.Left = value; } }
        public float PosY { get { return rect.Top; } set { rect.Top = value; } }

        public Vector2f Size { get { return new Vector2f(rect.Width, rect.Height); } set { Width = value.X; Height = value.Y; } }
        public Vector2f Pos { get { return new Vector2f(rect.Left, rect.Top); } set { PosX = value.X; PosY = value.Y; } }

        public Vector2f CenterPos { get { return new Vector2f(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2); } }


        public HitBox(Vector2f _pos, Vector2f _size)
        {
            rect = new FloatRect(_pos, _size);
        }


        public void UpdatePos(float posX, float posY)
        {
            Pos = new Vector2f(posX, posY);
        }

        public void UpdateSize(float width, float height)
        {
            Size = new Vector2f(width, height);
        }

        public void UpdatePos(Vector2f pos)
        {
            Pos = pos;
        }

        public void UpdateSize(Vector2f size)
        {
            Size = size;
        }


        public void Update(float posX, float posY, float width, float height)
        {
            Pos = new Vector2f(posX, posY);
            Size = new Vector2f(width, height);
        }

        public void Update(Vector2f pos, Vector2f size)
        {
            Pos = pos;
            Size = size;
        }
    }
}
