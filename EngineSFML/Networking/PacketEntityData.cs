using System;
using System.Collections.Generic;
using System.Text;

namespace EngineSFML.Networking
{
    public class PacketEntityData : Packet
    {

        private string entityID;
        public string EntityID { get { return entityID; } }

        private float posX;
        public float PosX { get { return posX; } }

        private float posY;
        public float PosY { get { return posY; } }

        private string addData;
        public string AddData { get { return addData; } }

        /*
         * entityData:entityID:posX:posY:addData 
        */
        public PacketEntityData(string _entityID, float _posX, float _posY, string _addData) : base ("entityData:" + _entityID + ":" + _posX + ":" + _posY + ":" + _addData, PacketType.entityData)
        {
            entityID = _entityID;

            posX = _posX;
            posY = _posY;

            addData = _addData;
        }

        public new static PacketEntityData ParsePacket(string _data)
        {
            _data = _data.Replace(";", "");
            string[] data = _data.Split(":");
            if (data[0] == "entityData")
            {
                return new PacketEntityData(data[1], float.Parse(data[2]), float.Parse(data[3]), data[4]);
            }
            return null;
        }

    }
}
