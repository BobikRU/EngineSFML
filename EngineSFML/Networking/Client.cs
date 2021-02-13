using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EngineSFML.Networking
{
    public class Client
    {

        private Thread receiveThread;

        private Socket socket;

        public Socket Socket { get { return socket; } }

        private string ipAddress;

        private string nickname;

        public event EventHandler<Server.DataReceivedArgs> HasReceived;

        public Client(string _ipAddress, string _nickname)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ipAddress = _ipAddress;

            nickname = _nickname;

            receiveThread = new Thread(ClientReceive);
            receiveThread.Name = "CLIENT RECEIVE";
            receiveThread.Start();
        }

        public void ClientReceive()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), Server.PORT);
            socket.Connect(endPoint);
            if (socket.Connected)
                Send(new PacketConnect(nickname));

            while (socket.Connected && Main.Game.Instance.IsStarted)
            {
                StringBuilder builder = new StringBuilder();
                int receivedCount = 0;
                byte[] receivedBytes = new byte[256];

                while (socket.Available > 0 && Main.Game.Instance.IsStarted)
                {
                    try
                    {
                        receivedCount = socket.Receive(receivedBytes);
                    }
                    catch
                    {
                        break;
                    }
                    builder.Append(Encoding.Unicode.GetString(receivedBytes, 0, receivedCount));
                }

                string[] splited = builder.ToString().Split(";");
                for (int i = 0; i < splited.Length; ++i)
                {
                    if (splited[i] != "" || splited[i] != " ")
                        HasReceived?.Invoke(null, new Server.DataReceivedArgs(Packet.GetPacket(splited[i].Replace(";", ""))));
                }
            }
        }

        public void Shutdown()
        {
            Send(new PacketDisconnect(nickname));
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket.Dispose();
        }

        public void Send(Packet packet)
        {
            if (socket.Connected)
            {
                socket.Send(Encoding.Unicode.GetBytes(packet.ToString()));
            }
        }

        

    }
}
