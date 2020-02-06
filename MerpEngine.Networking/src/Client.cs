using MerpEngine.Networking.Events;
using MerpEngine.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MerpEngine.Networking
{
    [Serializable]
    public class Client : Compoment
    {
        [NonSerialized]
        Socket master;
        public event EventHandler<FailEventArgs> OnFail;
        public event EventHandler OnConnected;
        public event EventHandler<DataRecieveEventArgs> OnRegistered;
        public event EventHandler<DataRecieveEventArgs> OnDataRecieved;

        public Guid id;

        public Client() : base()
        {
            Connect("192.168.0.224");
        }

        public void Connect(string ip)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 4242);
            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                master.Connect(ipe);
            }
            catch(Exception e)
            {
                OnFail?.Invoke(this, new FailEventArgs($"Could not connect to server: {ip}", e));
            }
            Thread t = new Thread(Data_IN);
            t.Start();
        }

        void Data_IN()
        {
            byte[] buffer;
            int readBytes;

            while (true)
            {
                buffer = new byte[master.SendBufferSize];
                readBytes = master.Receive(buffer);

                if(readBytes > 0)
                {
                    DataManager(Packets.Packet.FromBytes(buffer));
                }
            }
        }

        void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Registration:
                    OnRegistered(this, new DataRecieveEventArgs(p));
                    break;
                case PacketType.Create:

                    break;
                default:
                    Debug.Log("Recived registration");
                    OnDataRecieved(this, new DataRecieveEventArgs(p));
                    break;
            }
        }

        public void SyncGameobject(Packet p)
        {
            GameObject go = p.Gdata as GameObject;
            if(go != null)
            {
                GameObject ggo = LevelManager.LoadedLevel.GetGameObject(go.Name);
                if (ggo != null)
                {
                    ggo = go;
                }
                else
                {
                    CreateGameObject(p);
                }
            }
        }

        public void SyncCompoment(Packet p)
        {

        }

        void CreateGameObject(Packet p)
        {
            GameObject go = p.Gdata as GameObject;
            if (go != null)
            {
                LevelManager.LoadedLevel.GameObjects.Add(go);
            }
        }
    }
}
