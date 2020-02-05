using System;
using System.Net;

namespace MerpEngine.Networking.Interface{
    public interface IServer
    {
        void Start();
        void Stop();
        void kick(Guid id);
        void Ban(Guid id);
    }
}