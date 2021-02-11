using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;

using EngineSFML.Main;

using SFML.System;

namespace EngineSFML.GameObjects.Animations
{
    public abstract class Animation
    {

        public const float ANIM_Q = 0.005f;

        private GameObject animatedObject;
        public GameObject AnimatedObject { get { return animatedObject; } }

        private readonly int maxFrames;
        public int MaxFrames { get { return maxFrames; } }

        private bool isFlip = false;
        public bool IsFlip { get { return isFlip; } protected set { isFlip = value; } }

        private float currentFrame;
        public float CurrentFrame { get { return currentFrame; } protected set { currentFrame = value; } }

        private int layer;
        public int Layer { get {return layer; } protected set { layer = value; } }

        private float animSpeed;
        public float AnimationSpeed { get { return animSpeed; } protected set { animSpeed = value; } }

        private int sprWidth, sprHeight;
        
        public int SpriteWidth { get { return sprWidth; } protected set { sprWidth = value; } }
        public int SpriteHeight { get { return sprHeight; } protected set { sprHeight = value; } }

        protected Animation(GameObject _gameObjectAnim, int _maxFrames, int _spriteWidth, int _spriteHeight, float _speed)
        {
            animatedObject = _gameObjectAnim;
            currentFrame = 0;
            layer = 0;
            maxFrames = _maxFrames;

            sprWidth = _spriteWidth;
            sprHeight = _spriteHeight;

            animSpeed = _speed;
        }

        public virtual void Update()
        {
            currentFrame += animSpeed * MainWindow.Instance.DeltaTime * ANIM_Q;
            if (currentFrame > maxFrames)
                currentFrame = maxFrames - currentFrame;

            if (isFlip)
                AnimatedObject.Sprite.Scale = new Vector2f(-MathF.Abs(AnimatedObject.Sprite.Scale.X), AnimatedObject.Sprite.Scale.Y);
            else
                AnimatedObject.Sprite.Scale = new Vector2f(MathF.Abs(AnimatedObject.Sprite.Scale.X), AnimatedObject.Sprite.Scale.Y);

            animatedObject.Sprite.TextureRect = new IntRect(sprWidth * (int)currentFrame, sprHeight * layer, sprWidth, sprHeight);
        }

    }
}
