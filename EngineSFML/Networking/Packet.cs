using System;
using System.Collections.Generic;
using System.Text;

namespace EngineSFML.Networking
{
    public class Packet
    {
        public enum PacketType
        {
            raw,
            connect,
            disconnect,
            entityData
        }

        private PacketType packetType;
        public PacketType Type { get { return packetType; } }

        protected string data;

        public Packet(string _info, PacketType _type)
        {
            data = _info.Replace(";", "");
            packetType = _type;
        }

        public static Packet ParsePacket(string _data)
        {
            _data = _data.Replace(";", "");
            return new Packet(_data, PacketType.raw);
        }
        
        public static Packet GetPacket(string _data)
        {
            _data = _data.Replace(";", "");
            string[] packetSplit = _data.Split(":");
            switch (packetSplit[0])
            {
                case "connect":
                    return PacketConnect.ParsePacket(_data);
                case "entityData":
                    return PacketEntityData.ParsePacket(_data);
                case "disconnect":
                    return PacketDisconnect.ParsePacket(_data);
                default:
                    return new Packet(_data, PacketType.raw);
            }
        }

        public new string ToString()
        {
            return data + ";";
        }
    }
}
