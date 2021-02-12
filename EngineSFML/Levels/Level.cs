using System;
using System.Collections.Generic;
using System.Text;

using EngineSFML.GameObjects;
using EngineSFML.GameObjects.Blocks;
using EngineSFML.GameObjects.Entities;

using SFML.System;

using SFML.Graphics;
using EngineSFML.Main;
using EngineSFML.GUI;
using System.Xml;
using System.IO;

namespace EngineSFML.Levels
{
    public class Level
    {

        public struct XmlBlock
        {
            public Block.BlockName name;
            public Vector2f pos;

            public XmlBlock(Block.BlockName _name, Vector2f _pos)
            {
                name = _name;
                pos = _pos;
            }
        }

        public struct XmlEntity
        {
            public Entity.EntityName name;
            public Vector2f pos;

            public XmlEntity(Entity.EntityName _name, Vector2f _pos)
            {
                name = _name;
                pos = _pos;
            }
        }

        public struct CollisionState
        {
            public bool isCollided;
            public GameObject obj;
            public Utils.Direction side;

            public CollisionState(bool _isCollided, GameObject _obj = null, Utils.Direction _side = Utils.Direction.right)
            {
                isCollided = _isCollided;
                obj = _obj;
                side = _side;
            }
        }

        private List<Block> blocks;
        public List<Block> Blocks { get { return blocks; } }

        private List<Entity> entities;
        public List<Entity> Entities { get { return entities; } }

        public EntityPlayer Player { get; private set; }

        private readonly string levelname;
        public string LevelName { get { return levelname; } }

        private XmlDocument xmlDocument;

        private bool isLoadead;
        public bool IsLoaded { get { return isLoadead; } }

        private bool isPaused;
        public bool IsPaused { get { return isPaused; } set { isPaused = value; } }

        public Level(string _levelname)
        {

            blocks = new List<Block>();
            entities = new List<Entity>();

            levelname = _levelname;

            isLoadead = false;

            xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load("Resources\\Levels\\" + levelname + ".xml");
            }
            catch
            {
                CreateClearLevel();
                xmlDocument.Load("Resources\\Levels\\" + levelname + ".xml");
            }
        }

        private void CreateClearLevel()
        {
            if (!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");

            if (!Directory.Exists("Resources\\Levels"))
                Directory.CreateDirectory("Resources\\Levels");

            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null));

            XmlNode root = xmlDocument.CreateElement("level");

            XmlNode name = xmlDocument.CreateElement("name");
            name.InnerText = levelname;
            root.AppendChild(name);

            XmlNode blocks = xmlDocument.CreateElement("blocks");
            root.AppendChild(blocks);

            XmlNode entities = xmlDocument.CreateElement("entities");
            entities.AppendChild(GetXmlNodeEntity(new XmlEntity(Entity.EntityName.player, new Vector2f(320, 240))));
            root.AppendChild(entities);

            xmlDocument.AppendChild(root);

            xmlDocument.Save("Resources\\Levels\\" + levelname + ".xml");
        }

        public void LoadLevel()
        {
            if (!isLoadead)
            {
                XmlNode root = xmlDocument.DocumentElement;

                Func<XmlNode, bool> LoadBlocks = (XmlNode node) =>
                {
                    foreach (XmlNode xmlNode in node.ChildNodes)
                    {
                        XmlBlock xmlBlock = GetXmlBlock(xmlNode);
                        SetBlock(xmlBlock.name, xmlBlock.pos);
                    }
                    return true;
                };

                Func<XmlNode, bool> LoadEntities = (XmlNode node) =>
                {
                    foreach (XmlNode xmlNode in node.ChildNodes)
                    {
                        XmlEntity xmlEntity = GetXmlEntity(xmlNode);
                        SpawnEntity(xmlEntity.name, xmlEntity.pos);
                    }
                    return true;
                };

                foreach (XmlNode xmlNode in root)
                {
                    if (xmlNode.Name == "blocks")
                        LoadBlocks(xmlNode);

                    if (xmlNode.Name == "entities")
                        LoadEntities(xmlNode);
                }

                isLoadead = true;
            }
        }

        public void FullRestart()
        {
            if (isLoadead) 
            {
                Clear();
                isLoadead = false;
                LoadLevel();
            }
        }

        public void Clear()
        {
            Player = null;

            for (int i = 0; i < entities.Count; ++i)
                entities.RemoveAt(i);

            for (int i = 0; i < blocks.Count; ++i)
                blocks.RemoveAt(i);
        }

        private XmlBlock GetXmlBlock(XmlNode xmlNode)
        {
            string name = "";
            Vector2f pos = new Vector2f();

            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.Name == "name")
                    name = node.InnerText;

                if (node.Name == "posX")
                    pos.X = float.Parse(node.InnerText);

                if (node.Name == "posY")
                    pos.Y = float.Parse(node.InnerText);
            }

            Block.BlockName blockName = Block.BlockName.grass;

            for (int i = 0; i < Block.BlockNameCount; ++i)
            {
                if (((Block.BlockName)i).ToString() == name)
                    blockName = (Block.BlockName)i;
            }

            XmlBlock xmlBlock = new XmlBlock(blockName, pos);

            return xmlBlock;
        }

        private XmlNode GetXmlNodeBlock(XmlBlock xmlBlock)
        {
            XmlNode xmlNode = xmlDocument.CreateElement("block");
            
            xmlNode.AppendChild(xmlDocument.CreateElement("name")).InnerText = xmlBlock.name.ToString();

            xmlNode.AppendChild(xmlDocument.CreateElement("posX")).InnerText = xmlBlock.pos.X.ToString();
            xmlNode.AppendChild(xmlDocument.CreateElement("posY")).InnerText = xmlBlock.pos.Y.ToString();

            return xmlNode;
        }

        private XmlNode GetXmlNodeEntity(XmlEntity xmlEntity)
        {
            XmlNode xmlNode = xmlDocument.CreateElement("entity");

            xmlNode.AppendChild(xmlDocument.CreateElement("name")).InnerText = xmlEntity.name.ToString();

            xmlNode.AppendChild(xmlDocument.CreateElement("posX")).InnerText = xmlEntity.pos.X.ToString();
            xmlNode.AppendChild(xmlDocument.CreateElement("posY")).InnerText = xmlEntity.pos.Y.ToString();

            return xmlNode;
        }


        private XmlEntity GetXmlEntity(XmlNode xmlNode)
        {
            string name = "";
            Vector2f pos = new Vector2f(0, 0);

            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.Name == "name")
                    name = node.InnerText;

                if (node.Name == "posX")
                    pos.X = float.Parse(node.InnerText);

                if (node.Name == "posY")
                    pos.Y = float.Parse(node.InnerText);
            }

            Entity.EntityName entityName = Entity.EntityName.player;

            for (int i = 0; i < Entity.EntityNameCount; ++i)
            {
                if (((Entity.EntityName)i).ToString() == name)
                    entityName = (Entity.EntityName)i;
            }

            XmlEntity xmlEntity = new XmlEntity(entityName, pos);

            return xmlEntity;
        }

        public bool IsFree(Vector2f pos)
        {
            bool isfree = true;

            foreach (Entity entity in entities)
            {
                if (entity.HitBox.Rect.Contains(pos.X, pos.Y))
                    isfree = false;
            }

            foreach (Block block in blocks)
            {
                if (block.HitBox.Rect.Contains(pos.X, pos.Y))
                    isfree = false;
            }

            return isfree;
        }

        public bool SpawnEntity(Entity.EntityName name, Vector2f pos)
        {
            bool spawned = true;

            Entity entity = null;

            if (IsFree(pos))
            {
                switch (name)
                {
                    case Entity.EntityName.player:
                        if (Player == null)
                            entity = new EntityPlayer(pos);
                        else
                            spawned = false;
                        break;
                    default:
                        spawned = false;
                        break;
                }
            }
            else
                spawned = false;

            if (entity != null)
                spawned = AddEntity(entity);
            else
                spawned = false;

            if (entity != null && spawned && name == Entity.EntityName.player)
                Player = (EntityPlayer) entity;

            return spawned;
        }

        public bool SetBlock(Block.BlockName name, Vector2f pos)
        {
            bool setted;
            Block block = null;

            if (IsFree(pos))
            {
                switch (name)
                {
                    case Block.BlockName.grass:
                        block = new BlockGrass(pos);
                        break;
                }
            }

            setted = block != null && AddBlock(block);

            return setted;
        }

        public void RemoveGameObject(GameObject obj)
        {
            if (obj.ObjectType == GameObject.Type.block)
            {
                if (blocks.Contains((Block)obj))
                    blocks.Remove((Block)obj);
            }

            if (obj.ObjectType == GameObject.Type.entity)
            {
                if (entities.Contains((Entity)obj))
                    entities.Remove((Entity)obj);
            }
        }

        private bool AddEntity(Entity entity)
        {
            bool added = !entities.Contains(entity);
            if (added)
                entities.Add(entity);
            return added;
        }

        private bool AddBlock(Block block)
        {
            bool added = !blocks.Contains(block);
            if (added)
                blocks.Add(block);
            return added;
        }

        public void Update()
        {
            if (!isPaused)
            {
                for (int i = 0; i < entities.Count; ++i)
                {
                    entities[i].Update();
                    if (i < entities.Count)
                        CheckCollsion(entities[i], out _);
                }

                for (int i = 0; i < blocks.Count; ++i)
                {
                    blocks[i].Update();
                    if (i < blocks.Count)
                        CheckCollsion(blocks[i], out _);
                }
            }
        }

        public void CheckCollsion(GameObject obj, out CollisionState _state)
        {
            _state = new CollisionState(false);
            foreach (GameObject obj2 in blocks)
            {

                if (obj2 != obj)
                {
                    if (obj.HitBox.Rect.Intersects(obj2.HitBox.Rect))
                        ObjectsCollided(obj, obj2, out _state);
                }

            }

            foreach (GameObject obj2 in entities)
            {
                if (obj2 != obj)
                {
                    if (obj.HitBox.Rect.Intersects(obj2.HitBox.Rect))
                        ObjectsCollided(obj, obj2, out _state);
                }
            }
        }

        private void ObjectsCollided(GameObject obj, GameObject obj2, out CollisionState _state)
        {
            FloatRect overlap;
            obj.HitBox.Rect.Intersects(obj2.HitBox.Rect, out overlap);

            float colX = 0;
            float colY = 0;

            if (overlap.Width < overlap.Height)
            {
                colX = -1.0f;
                if (obj.HitBox.CenterPos.X < obj2.HitBox.CenterPos.X)
                    colX = 1.0f;
            } else
            {
                colY = 1.0f;
                if (obj.HitBox.CenterPos.Y > obj2.HitBox.CenterPos.Y)
                    colY = -1.0f;
            }


            Vector2f col = new Vector2f(colX, colY);

            float[] angles = Utils.AnalazeVector(col);

            Utils.Direction side = (Utils.Direction)Utils.GetMaxIndexInArray(angles);

            _state = new CollisionState(true, obj2, side);
            obj.CollidedWith(obj2, side);

            float dx = 0, dy = 0;

            if (obj.ObjectType != GameObject.Type.block)
            {
                switch (side)
                {
                    case Utils.Direction.top:
                        dy = -overlap.Height;
                        break;
                    case Utils.Direction.bottom:
                        dy = overlap.Height;
                        break;
                    case Utils.Direction.right:
                        dx = overlap.Width;
                        break;
                    case Utils.Direction.left:
                        dx = -overlap.Width;
                        break;
                }

                obj.Pos = new Vector2f(obj.PosX + dx, obj.PosY + dy);
            }
        }

        public void Draw()
        {
            for (int i = 0; i < blocks.Count; ++i)
            {
                blocks[i].Draw();
            }

            for (int i = 0; i < entities.Count; ++i)
            {
                entities[i].Draw();
            }
        }

    }
}
