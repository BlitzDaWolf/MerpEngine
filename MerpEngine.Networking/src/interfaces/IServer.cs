using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MerpEngine.Networking.Interface
{
    public interface IServer
    {
        List<User> Users { get; set; }
        static IServer Instance { get; internal set; }

        void Start();
        void Stop();

        void Tick();

        void Kick(Guid id);
        void Kick(string name);
        void Ban(Guid id);
        void Ban(string name);
    }
}