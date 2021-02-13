using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;
using SFML.System;

using EngineSFML.Main;
using EngineSFML.GameObjects.Animations;
using EngineSFML.GUI;

namespace EngineSFML.GameObjects.Entities
{
    public class EntityPlayerMP : Entity
    {

        private AnimationPlayer animation;

        private bool isMoving;
        private Utils.Direction moveDir;

        private string playerName;
        public string PlayerName { get { return playerName; } }

        private Text headText;

        public EntityPlayerMP(Vector2f pos, string _name) : base(pos, new Vector2f(36, 72), "Resources\\Sprites\\Entities\\player.png", EntityName.playerMP)
        {
            playerName = _name;
            Console.WriteLine(_name);

            isMoving = false;
            moveDir = Utils.Direction.right;

            Speed = 0.1f;

            animation = new AnimationPlayer(this);

            headText = new Text(playerName, Canvas.Instance.font)
            {
                CharacterSize = 9,
                Position = new Vector2f(pos.X, pos.Y - 10)
            };
        }

        public void SetMove(Utils.Direction dir, bool _isMoving)
        {
            moveDir = dir;
            isMoving = _isMoving;
        }

        public override void Draw()
        {
            base.Draw();
            MainWindow.Instance.RenderWindow.Draw(headText);
        }

        public override void Update()
        {
            if (isMoving) 
            {
                if (moveDir == Utils.Direction.top)
                {
                    animation.State = AnimationPlayer.PlayerState.runTop;
                }
                else if (moveDir == Utils.Direction.bottom)
                {
                    animation.State = AnimationPlayer.PlayerState.runBottom;
                }
                else if (moveDir == Utils.Direction.right)
                {
                    animation.State = AnimationPlayer.PlayerState.runRight;
                }
                else if (moveDir == Utils.Direction.left)
                {
                    animation.State = AnimationPlayer.PlayerState.runLeft;
                }
            }
            else
            {
                animation.State = AnimationPlayer.PlayerState.idle;
            }

            animation.Update();

            base.Update();

            headText.Position = new Vector2f(PosX, PosY - 10);
        }

    }
}
