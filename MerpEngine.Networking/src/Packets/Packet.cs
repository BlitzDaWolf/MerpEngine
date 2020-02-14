using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MerpEngine.Networking.Packets
{
    [Serializable]
    public class Packet
    {
        public object Gdata;
        public int packetInt;
        public bool packetBool;
        public Guid senderID;
        public PacketType packetType;
        public NetworkSentType NetworkSentType;

        public Packet(PacketType type, Guid senderID)
        {
            Gdata = new List<string>();
            this.senderID = senderID;
            this.packetType = type;
        }

        public static Packet FromBytes(byte[] packetBytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(packetBytes);

            Packet p = (Packet)bf.Deserialize(ms);
            ms.Close();
            return p;
        }

        public byte[] ToBytes()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, this);
            byte[] bytes = ms.ToArray();
            ms.Close();

            return bytes;
        }

        public static string GetIP4Address()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var item in ips)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return item.ToString();
            }
            return "127.0.0.1";
        }
    }

    public enum PacketType
    {
        Registration,
        Create,
        Sync
    }

    public enum NetworkSentType
    {
        ServerOnly,
        Clients
    }
}
