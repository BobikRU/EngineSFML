using System;
using System.Collections.Generic;
using System.Text;

using EngineSFML.GameObjects.Entities;

namespace EngineSFML.GameObjects.Animations
{
    public class AnimationPlayer : Animation
    {

        public enum PlayerState
        {
            idle,
            runRight,
            runLeft,
            runTop,
            runBottom
        }

        private PlayerState state;
        public PlayerState State { set { state = value; } get { return state; } }

        public AnimationPlayer(Entity _plr) : base(_plr, 4, 36, 72, 0.1f)
        {
            state = PlayerState.idle;
        }

        public override void Update()
        {
            switch (state)
            {
                case PlayerState.idle:
                    Layer = 0;
                    AnimationSpeed = 0.25f;
                    IsFlip = false;
                    AnimatedObject.HitBox.Size = new SFML.System.Vector2f(36, 72);
                    break;
                case PlayerState.runBottom:
                    Layer = 1;
                    AnimationSpeed = 1f;
                    IsFlip = false;
                    AnimatedObject.HitBox.Size = new SFML.System.Vector2f(36, 72);
                    break;
                case PlayerState.runTop:
                    Layer = 2;
                    AnimationSpeed = 1f;
                    IsFlip = false;
                    AnimatedObject.HitBox.Size = new SFML.System.Vector2f(36, 72);
                    break;
                case PlayerState.runRight:
                    Layer = 3;
                    AnimationSpeed = 1f;
                    IsFlip = false;
                    AnimatedObject.HitBox.Size = new SFML.System.Vector2f(12, 72);
                    break;
                case PlayerState.runLeft:
                    Layer = 3;
                    AnimationSpeed = 1f;
                    IsFlip = true;
                    AnimatedObject.HitBox.Size = new SFML.System.Vector2f(36, 72);
                    break;
            }

            base.Update();
        }

    }
}
