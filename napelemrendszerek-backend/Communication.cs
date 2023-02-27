using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Comm
{
    [Serializable]
    class Communication
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public Communication() { }
        public Communication(string msg)
        {
            this.Message = msg;
            this.Date = DateTime.Now;
        }

        public override string ToString()
        {
            return Message + "[" + Date.ToString() + "]";
        }
    }
}
