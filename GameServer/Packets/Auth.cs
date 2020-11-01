using System;
using System.Collections.Generic;
using System.IO;
using ENet;
using GameServer.Utils;


namespace GameServer.Packets
{
    public class Auth
    {
        public static void RegisterAck(ref Event netEvent, uint playerId)
        {
            Log.Send(PrintState.Packet, $"RegisterReq from ID: {playerId} IP: {netEvent.Peer.IP}");
        }
    }
}
