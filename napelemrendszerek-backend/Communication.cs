using napelemrendszerek_backend.DBModels;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Comm
{
    [Serializable]
    class Communication
    {
        public string requestName { get; set; }
        public DateTime Date { get; set; }
        public object parameterObject { get; set; }
        public int roleId { get; set; }

        public Communication() { }
        public Communication(string request, object param, int roleId)
        {
            this.requestName = request;
            this.Date = DateTime.Now;
            this.parameterObject = param;
            this.roleId = roleId;
        }

        public override string ToString()
        {
            return requestName + "[" + Date.ToString() + "]";
        }
    }
    
}
