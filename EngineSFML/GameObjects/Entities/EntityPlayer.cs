using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;
using SFML.System;

using EngineSFML.Main;
using EngineSFML.GameObjects.Animations;

namespace EngineSFML.GameObjects.Entities
{
    public class EntityPlayer : Entity
    {

        private AnimationPlayer animation;

        public EntityPlayer(Vector2f pos) : base(pos, new Vector2f(36, 72), "Resources\\Sprites\\Entities\\player.png", EntityName.player)
        {
            Speed = 0.1f;

            animation = new AnimationPlayer(this);
        }

        public override void Update()
        {
            float dx = 0, dy = 0;

            if (Inputs.Instance.IsKeyPressed("up"))
            {
                dy = -1.0f;
                animation.State = AnimationPlayer.PlayerState.runTop;
            }
            else if (Inputs.Instance.IsKeyPressed("down"))
            {
                dy = 1.0f;
                animation.State = AnimationPlayer.PlayerState.runBottom;
            }
            else if (Inputs.Instance.IsKeyPressed("right"))
            {
                dx = 1.0f;
                animation.State = AnimationPlayer.PlayerState.runRight;
            }
            else if (Inputs.Instance.IsKeyPressed("left"))
            {
                dx = -1.0f;
                animation.State = AnimationPlayer.PlayerState.runLeft;
            }
            else
            {
                animation.State = AnimationPlayer.PlayerState.idle;
            }

            if (!Move(new Vector2f(dx, dy)))
                animation.State = AnimationPlayer.PlayerState.idle;
            animation.Update();

            base.Update();
        }

    }
}
