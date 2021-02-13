using System;
using System.Collections.Generic;
using System.Text;

namespace EngineSFML.Networking
{
    public class PacketConnect : Packet
    {

        private string nickname;
        public string Nickname { get { return nickname; } }

        public PacketConnect(string _nickname) : base("connect:" + _nickname, PacketType.connect)
        {
            nickname = _nickname;
        }

        public new static PacketConnect ParsePacket(string _data)
        {
            _data = _data.Replace(";", "");
            string[] data = _data.Split(":");
            if (data[0] == "connect")
            {
                return new PacketConnect(data[1]);
            }
            return null;
        }

    }
}
