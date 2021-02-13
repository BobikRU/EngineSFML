using System;
using System.Collections.Generic;
using System.Text;

namespace EngineSFML.Networking
{
    public class PacketDisconnect : Packet
    {
        private string nickname;
        public string Nickname { get { return nickname; } }

        public PacketDisconnect(string _nickname) : base("disconnect:" + _nickname, PacketType.disconnect)
        {
            nickname = _nickname;
        }

        public new static PacketDisconnect ParsePacket(string _data)
        {
            _data = _data.Replace(";", "");
            string[] data = _data.Split(":");
            if (data[0] == "disconnect")
            {
                return new PacketDisconnect(data[1]);
            }
            return null;
        }
    }
}
