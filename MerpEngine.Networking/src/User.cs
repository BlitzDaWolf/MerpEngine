using MerpEngine.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MerpEngine.Networking
{
    [Serializable]
    public class User
    {
        public string name;
        public Guid id;

        public Socket clientSocket;
        public Thread clientThread;

        public User()
        {
            id = Guid.NewGuid();
            clientThread = new Thread(HeadlessServer.Data_IN);
            clientThread.Start(clientSocket);
            SendRegistrationPacket();
        }
        public User(Socket s) : base()
        {
            clientSocket = s;
        }

        public void SendRegistrationPacket()
        {
            Packet p = new Packet(PacketType.Registration, id);

            clientSocket.Send(p.ToBytes());
        } 
    }
}
