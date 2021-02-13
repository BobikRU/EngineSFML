using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EngineSFML.Networking
{
    public class Server
    {
        public const int PORT = 12005;

        private Thread listenThread;

        private Thread dataThread;

        private Socket socket;

        public struct Player
        {
            public Socket socket;
            public string nickname;

            public Player(Socket _socket, string _nickname)
            {
                Console.WriteLine(_nickname);
                socket = _socket;
                nickname = _nickname;
            }
        }
        
        private List<Player> players;

        private string nickname;

        public class DataReceivedArgs : EventArgs
        {
            public Packet packet;

            public DataReceivedArgs(Packet _packet)
            {
                packet = _packet;
            }
        }

        public event EventHandler<DataReceivedArgs> HasReceived;

        public Server(string _nickname)
        {

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            players = new List<Player>();

            nickname = _nickname;

            listenThread = new Thread(ServerListing);
            listenThread.Name = "SERVER LISTEN";
            listenThread.Start();

            dataThread = new Thread(ServerData);
            dataThread.Name = "SERVER DATA";
            dataThread.Start();
        }

        public void ServerData()
        {
            while (Main.Game.Instance.IsStarted && socket != null)
            {
                for (int i = 0; i < players.Count; ++i)
                {
                    if (players[i].socket.Available > 0 && Main.Game.Instance.IsStarted)
                    {
                        StringBuilder builder = new StringBuilder();
                        int recivedCount = 0;
                        byte[] recivedBytes = new byte[256];

                        while (players[i].socket.Available > 0 && Main.Game.Instance.IsStarted)
                        {
                            try
                            {
                                recivedCount = players[i].socket.Receive(recivedBytes);
                            }
                            catch
                            {
                                
                            }
                            builder.Append(Encoding.Unicode.GetString(recivedBytes, 0, recivedCount));
                        }

                        Packet packet = Packet.GetPacket(builder.ToString());
                        Send(packet, players[i]);
                        string[] splited = builder.ToString().Split(";");
                        for (int j = 0; j < splited.Length; ++j)
                        {
                            if (splited[j] != "" || splited[j] != " ")
                                HasReceived?.Invoke(players[i], new Server.DataReceivedArgs(Packet.GetPacket(splited[j].Replace(";", ""))));
                        }
                    }

                    if (players[i].socket == null || !players[i].socket.Connected)
                        players.RemoveAt(i);
                }
            }
        }

        public void Shutdown()
        {
            for (int i = 0; i < players.Count; ++i)
            {
                players[i].socket.Shutdown(SocketShutdown.Both);
                players[i].socket.Close();
                players[i].socket.Dispose();
            }

            socket.Close();
            socket.Dispose();
        }

        public void Send(Packet packet, Player sender = new Player())
        {
            for (int i = 0; i < players.Count; ++i)
            {
                if (players[i].socket.Connected && players[i].socket != sender.socket)
                    try
                    {
                        players[i].socket.Send(Encoding.Unicode.GetBytes(packet.ToString()));
                    }
                    catch
                    {
                     
                    }
            }
        }

        public void SendTo(Socket receiver, Packet packet)
        {
            receiver.Send(Encoding.Unicode.GetBytes(packet.ToString()));
        }

        public void ServerListing()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            socket.Bind(endPoint);
            socket.Listen(5);
            while (Main.Game.Instance.IsStarted && socket != null)
            {
                Socket handler = null;
                try
                {
                    handler = socket.Accept();
                }
                catch 
                {
                    break;
                }

                StringBuilder builder = new StringBuilder();
                int recivedCount = 0;
                byte[] recivedBytes = new byte[256];

                do
                {
                    recivedCount = handler.Receive(recivedBytes);
                    builder.Append(Encoding.Unicode.GetString(recivedBytes, 0, recivedCount));
                } while (handler.Available > 0 && Main.Game.Instance.IsStarted);

                Packet packetConnect = Packet.GetPacket(builder.ToString());
                HasReceived?.Invoke(handler, new DataReceivedArgs(packetConnect));
                for (int i = 0; i < players.Count; ++i)
                {
                    SendTo(handler, new PacketConnect(players[i].nickname));
                }
                SendTo(handler, new PacketConnect(nickname));
                players.Add(new Player(handler, ((PacketConnect)packetConnect).Nickname));
            }
        }

    }
}
