using MerpEngine.Networking.Interface;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using MerpEngine.Networking.Packets;
using System.Linq;

namespace MerpEngine.Networking
{
    public class HeadlessServer : IServer
    {
        public List<User> Users { get; set; }

        private Socket serverSocket;
        private bool Running;

        public void Ban(Guid id)
        {

        }

        public void Ban(string name)
        {

        }

        public void Kick(Guid id)
        {

        }

        public void Kick(string name)
        {

        }

        public void Start()
        {
            IServer.Instance = this;
            Running = true;

            Users = new List<User>();
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(Packets.Packet.GetIP4Address()), 4242);
            serverSocket.Bind(ip);

            Debug.Log($"Started server @{ip}");

            Thread listenThread = new Thread(ListenThread);
            listenThread.Start();
        }

        void ListenThread()
        {
            while (Running)
            {
                serverSocket.Listen(0);
                Users.Add(new User(serverSocket.Accept()));
                Debug.Log($"A new user has joined the server");
            }
        }

        internal static void Data_IN(object cSocket)
        {
            Socket clientSocket = (Socket)cSocket;
            byte[] Buffer;
            int readBytes;

            for(; ; )
            {
                Buffer = new byte[clientSocket.SendBufferSize];
                readBytes = clientSocket.Receive(Buffer);

                if(readBytes > 0)
                {
                    Packet packet = Packet.FromBytes(Buffer);
                    DataManager(packet);
                }
            }
        }

        internal static void DataManager(Packet packet)
        {
            switch (packet.NetworkSentType)
            {
                case NetworkSentType.ServerOnly:

                    break;
                case NetworkSentType.Clients:
                    IServer.Instance.Users.Where(x => x.id != packet.senderID)
                        .ToList().ForEach(x => x.clientSocket.Send(packet.ToBytes()));
                    break;
                default:
                    break;
            }
        }

        public void Stop()
        {

        }

        public void Tick()
        {

        }
    }
}