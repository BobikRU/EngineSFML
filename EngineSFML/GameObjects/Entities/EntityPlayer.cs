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

            bool isMoving = false;

            Utils.Direction dir = Utils.Direction.right;

            if (Inputs.Instance.IsKeyPressed("up"))
            {
                dy = -1.0f;
                animation.State = AnimationPlayer.PlayerState.runTop;
                dir = Utils.Direction.top;
            }
            else if (Inputs.Instance.IsKeyPressed("down"))
            {
                dy = 1.0f;
                animation.State = AnimationPlayer.PlayerState.runBottom;
                dir = Utils.Direction.bottom;
            }
            else if (Inputs.Instance.IsKeyPressed("right"))
            {
                dx = 1.0f;
                animation.State = AnimationPlayer.PlayerState.runRight;
                dir = Utils.Direction.right;
            }
            else if (Inputs.Instance.IsKeyPressed("left"))
            {
                dx = -1.0f;
                animation.State = AnimationPlayer.PlayerState.runLeft;
                dir = Utils.Direction.left;
            }
            else
            {
                animation.State = AnimationPlayer.PlayerState.idle;
            }

            if (!Move(new Vector2f(dx, dy)))
                animation.State = AnimationPlayer.PlayerState.idle;
            animation.Update();

            base.Update();

            if (dx != 0 || dy != 0)
                isMoving = true;

            string addData =  Game.Instance.PlayerName + "." + isMoving.ToString() + "." + ((int)dir).ToString();
            if (Game.Instance.IsServer)
                Game.Instance.Sever.Send(new Networking.PacketEntityData(this.EntityID, PosX, PosY, addData));
            else
                Game.Instance.Client.Send(new Networking.PacketEntityData(this.EntityID, PosX, PosY, addData));
        }

    }
}
