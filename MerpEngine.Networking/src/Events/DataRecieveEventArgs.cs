using MerpEngine.Networking.Packets;
using System;

namespace MerpEngine.Networking.Events
{
    public class DataRecieveEventArgs : EventArgs
    {
        public bool Success { get; }
        public Packet Packet { get; }

        public DataRecieveEventArgs(Packet p)
        {
            Packet = p;
            Success = true;
        }
    }
}
