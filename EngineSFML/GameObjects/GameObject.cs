using System;
using System.Collections.Generic;
using System.Text;
using EngineSFML.Main;
using SFML.Graphics;
using SFML.System;

namespace EngineSFML.GameObjects
{
    public abstract class GameObject
    {
        public enum Type
        {
            entity,
            block
        }

        private Type objectType;
        public Type ObjectType  { get { return objectType; } }

        private string tag;
        public string ObjectTag { get { return tag; } }

        protected Texture texture;
        public Texture Texture { get { return texture; } }

        protected Sprite sprite;
        public Sprite Sprite { get { return sprite; } }
        public Color Color { get { return sprite.Color; } set { sprite.Color = value; } }

        protected HitBox hitbox;
        public HitBox HitBox { get { return hitbox; } }

        public float Width { get { return HitBox.Width; } private set { HitBox.Width = value; } }
        public float Height { get { return HitBox.Height; } private set { HitBox.Height = value; } }
        public Vector2f Size { get { return HitBox.Size; } set { Width = value.X; Height = value.Y; } }

        public float PosX { get { return Sprite.Position.X; } private set { Sprite.Position = new Vector2f(value, Sprite.Position.Y); HitBox.PosX = value - sprite.Origin.X; } }
        public float PosY { get { return Sprite.Position.Y; } private set { Sprite.Position = new Vector2f(Sprite.Position.X, value); HitBox.PosY = value - sprite.Origin.Y; } }
        public Vector2f Pos { get { return new Vector2f(PosX, PosY); } set { PosX = value.X; PosY = value.Y; } }

        protected GameObject(Vector2f _pos, Vector2f _size, string _filename, Type _type, string _tag)
        {
            objectType = _type;
            tag = _tag;

            texture = new Texture(_filename);
            sprite = new Sprite(Texture);

            hitbox = new HitBox(_pos, _size);

            Pos = _pos;
            Size = _size;

            sprite.Origin = new Vector2f((float)sprite.TextureRect.Width / 2.0f, (float)sprite.TextureRect.Height / 2.0f);
        }

        public virtual void Update()
        {
            sprite.Origin = new Vector2f((float)sprite.TextureRect.Width / 2.0f, (float)sprite.TextureRect.Height / 2.0f);
        }

        public virtual void Draw()
        {
            MainWindow.Instance.RenderWindow.Draw(sprite);
        }

        public virtual void Destroy()
        {
            Game.Instance.Level.RemoveGameObject(this);
        }

        public virtual void CollidedWith(GameObject obj2, Utils.Direction side)
        {
        }
    }
}
