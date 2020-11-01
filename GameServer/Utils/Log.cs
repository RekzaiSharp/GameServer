using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameServer.Utils
{
    public enum PrintState
    {
        Information,
        Warning,
        Database,
        Critical,
        Packet,
        Log
    }


    public class Log
    {

        private static readonly List<ConsoleColor> _printColors = new List<ConsoleColor>() {ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.DarkRed, ConsoleColor.Magenta, ConsoleColor.White};
        public static void Send(PrintState state, string message)
        {
            Console.ForegroundColor = _printColors[(int) state];
            Console.Write("[" + state.ToString() + "]: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message + "\n");
        }

    }
}
