using System;

namespace MerpEngine.Networking.Events
{
    public class FailEventArgs : EventArgs
    {
        public string Message { get; }
        public Exception Exception { get; }
        public FailEventArgs(string msg, Exception ex) : base()
        {
            Message = msg;
            Exception = ex;
        }
    }
}
