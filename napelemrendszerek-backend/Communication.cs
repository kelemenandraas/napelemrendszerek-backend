using napelemrendszerek_backend.Models;
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
        public object contentObject { get; set; }
        public int? roleId { get; set; }

        public Communication() { }
        public Communication(string request, object param, int roleId)
        {
            this.Message = request;
            this.Date = DateTime.Now;
            this.contentObject = param;
            this.roleId = roleId;
        }
        

        public override string ToString()
        {
            return Message + "[" + Date.ToString() + "]";
        }
    }
    
}
