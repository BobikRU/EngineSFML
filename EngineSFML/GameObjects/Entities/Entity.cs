using System;
using System.Collections.Generic;
using System.Text;

using SFML.System;
using SFML.Graphics;

using EngineSFML.Main;
using EngineSFML.Levels;

namespace EngineSFML.GameObjects.Entities
{
    public abstract class Entity : GameObject
    {

        public static int lastIntID = 0;
        public static List<string> entitiesIDs = new List<string>();

        protected string entityID;
        public string EntityID { get { return entityID; } }

        public const int EntityNameCount = 1;
        public enum EntityName
        {
            player,
            playerMP
        }

        protected float maxHealth;
        private float health;
        public float Health { get { return health; } set { health = value <= maxHealth ? value : maxHealth; } }

        private float speed;
        public float Speed { get { return speed; } set { speed = value; } }

        protected Entity(Vector2f _pos, Vector2f _size, string _filename, EntityName _name) : base(_pos, _size, _filename, Type.entity, _name.ToString())
        {
            entityID = _name.ToString() + "_" + lastIntID;
            entitiesIDs.Add(entityID);
            lastIntID++;

            maxHealth = 100;
            health = maxHealth;

            speed = 0.07f;
        }

        public override void Destroy()
        {
            entitiesIDs.Remove(entityID);
            base.Destroy();
        }

        public override void Update()
        {

            if (health <= 0)
                Destroy();

            if (health > maxHealth)
                health = maxHealth;

            base.Update();
        }

        public bool Move(Vector2f dir)
        {
            bool moved = true;

            float dx = dir.X * speed * Main.MainWindow.Instance.DeltaTime;
            float dy = dir.Y * speed * Main.MainWindow.Instance.DeltaTime;

            Pos = new Vector2f(PosX + dx, PosY + dy);

            Game.Instance.Level.CheckCollsion(this, out Level.CollisionState state);
            if (state.isCollided)
            {
                if (dir.X > 0 && state.side == Utils.Direction.left)
                    moved = false;
                if (dir.X < 0 && state.side == Utils.Direction.right)
                    moved = false;
                if (dir.Y > 0 && state.side == Utils.Direction.top)
                    moved = false;
                if (dir.Y < 0 && state.side == Utils.Direction.bottom)
                    moved = false;
            }

            return moved;
        }
    }
}
