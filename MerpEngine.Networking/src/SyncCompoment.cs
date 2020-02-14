using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MerpEngine.Networking
{
    [Serializable]
    public class SyncCompoment : Component
    {
        public Guid NetworkID { get; private set; }
        public Guid UserID { get; private set; }

        public bool ServerOnly { get; private set; }

        public void Sync()
        {

        }
    }
}
