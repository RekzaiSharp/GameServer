using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ENet;
using GameServer.Utils;

namespace GameServer.Packets
{
    public class PacketHandler
    {
        public enum PacketId : byte
        {
            RegisterReq = 1,
            RegisterAck = 2,
            LoginReq = 3,
            LoginAck = 4,
            LoginEvent = 5
        }

        public static void HandlePacket(ref Event netEvent)
        {
            var readBuffer = new byte[1024];
            var readStream = new MemoryStream(readBuffer);
            var reader = new BinaryReader(readStream);

            readStream.Position = 0;
            netEvent.Packet.CopyTo(readBuffer);
            var packetId = (PacketId)reader.ReadByte();

            switch (packetId)
            {
                case PacketId.RegisterReq:
                    Auth.RegisterAck(ref netEvent, netEvent.Peer.ID);
                    break;
            }
        }
    }
}
