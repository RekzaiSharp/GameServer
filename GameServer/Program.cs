using System;
using System.Collections.Generic;
using ENet;
using GameServer.Classes;
using GameServer.Packets;
using GameServer.Utils;

/*
 *    Game Ideas
 *    Killer: Killers have a lot of health and can deal a lot of damage, 
 *
 *
 *
 * 
 */

namespace GameServer
{

    static class Program
    {
        static Host _server = new Host();
        private static Dictionary<uint, Account> _players = new Dictionary<uint, Account>();
        private static ushort _Port = 6006;
        private static byte _maxPlayers = 200;
        private static string _serverVersion = "0.1dev";

        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            Log.Send(PrintState.Information, $"Game Server {_serverVersion} - Started at {DateTime.Now}");
            Library.Initialize(); // Initialize ENet Library

            Address address = new Address {Port = _Port}; // Setting IP & Port
            _server = new Host(); // Creating UDP Host
            _server.Create(address, _maxPlayers); // Creating Server with Connection Limit

            Log.Send(PrintState.Information, $"Game Server started on Port {_Port}..");

            Event netEvent;
            while (!Console.KeyAvailable)
            {
                bool polled = false;

                while (!polled)
                {
                    if (_server.CheckEvents(out netEvent) <= 0)
                    {
                        if (_server.Service(15, out netEvent) <= 0)
                            break;

                        polled = true;
                    }

                    switch (netEvent.Type)
                    {
                        case EventType.None:
                            break;
                        case EventType.Connect:
                            Log.Send(PrintState.Packet, "Connection from ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            netEvent.Peer.Timeout(32, 1000, 4000);
                            break;
                        case EventType.Disconnect:
                            Console.WriteLine("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            break;
                        case EventType.Timeout:
                            Log.Send(PrintState.Warning, "Timeout from ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            break;
                        case EventType.Receive:
                            PacketHandler.HandlePacket(ref netEvent);
                            netEvent.Packet.Dispose();
                            break;
                    }
                }

                _server.Flush();
            }
            Library.Deinitialize();
        }
    }
}
