using System;
using System.Collections.Generic;
using System.Text;

using EngineSFML.GameObjects.Entities;
using EngineSFML.GameObjects.Blocks;

using SFML.System;
using SFML.Graphics;

using EngineSFML.Levels;

using EngineSFML.GUI;
using EngineSFML.Networking;

namespace EngineSFML.Main
{
    public sealed class Game
    {
        private static readonly Lazy<Game> instance =
            new Lazy<Game>(() => new Game());
        public static Game Instance { get { return instance.Value; } }

        private bool isStarted;
        public bool IsStarted { get { return isStarted; } }

        private MainWindow mainWindow;
        public MainWindow Window { get { return mainWindow; } }

        private bool isDebug;
        public bool IsDebug { get { return isDebug; } }

        private Level level;
        public Level Level { get { return level; } }

        private bool isServer;
        public bool IsServer { get { return isServer; } }

        private Server server;
        public Server Sever { get { return server; } }

        private Client client;
        public Client Client { get { return client; } }

        private string playerName;
        public string PlayerName { get { return playerName; } }


        private Game()
        {
            isStarted = false;

            mainWindow = MainWindow.Instance;

            mainWindow.RenderWindow.Closed += (obj, e) =>
            {
                isStarted = false;
                if (isServer && server != null)
                {
                    server.Shutdown();
                } else if (!isServer && client != null)
                {
                    client.Shutdown();
                }
            };

            mainWindow.Update += Update;
            mainWindow.Draw += Draw;
        }

        private void Init()
        {
            Canvas.Instance.AddGUI(new MainMenu());
        }
        
        public void Update()
        {
            if (level != null)
                level.Update();
            Canvas.Instance.Update();
        }

        public void Draw()
        {
            if (level != null)
                level.Draw();
            Canvas.Instance.Draw();
        }

        public void ConnectRoom(string _nickname, string _ip)
        {
            playerName = _nickname;

            isServer = false;
            server = null;

            mainWindow.RenderWindow.SetTitle("CLIENT");

            client = new Client(_ip, _nickname);
            client.HasReceived += HasReceived;

            while (!client.Socket.Connected) ;

            level = new Level("test");
            level.LoadLevel();
        }

        public void CreateRoom(string _nickname)
        {
            playerName = _nickname;

            isServer = true;
            client = null;

            mainWindow.RenderWindow.SetTitle("SERVER");

            server = new Server(_nickname);
            server.HasReceived += HasReceived;

            level = new Level("test");
            level.LoadLevel();
        }

        public void HasReceived(object sender, Server.DataReceivedArgs args)
        {
            if (args.packet != null)
            {
                switch (args.packet.Type)
                {
                    case Packet.PacketType.connect:
                        level.SpawnEntity(Entity.EntityName.playerMP, new Vector2f(0, 0), ((PacketConnect)args.packet).Nickname);
                        break;
                    case Packet.PacketType.entityData:
                        level.UpdateEntityByPacket((PacketEntityData)args.packet);
                        break;
                    case Packet.PacketType.disconnect:
                        EntityPlayerMP plr = level.GetPlayerAtName(((PacketDisconnect)args.packet).Nickname);
                        if (plr != null)
                            plr.Destroy();
                        break;
                    default:
                        //Console.WriteLine(args.packet.ToString());
                        break;
                }
            }
        }

        public void Start(bool _isDebug)
        {
            if (!isStarted)
            {
                isDebug = _isDebug;
                isStarted = true;
                Init();
                mainWindow.StartLoop();
            }
        }

    }
}
