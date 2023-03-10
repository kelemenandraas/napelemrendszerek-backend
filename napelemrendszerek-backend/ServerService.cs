using Comm;
using napelemrendszerek_backend;
using napelemrendszerek_backend.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
namespace SocketServer
{
    public class AsyncService
    {
        private IPAddress ipAddress;
        private int port;
        public AsyncService(int port)
        {
            this.port = port;

            this.ipAddress = IPAddress.Loopback;
            if (this.ipAddress == null)
                throw new Exception("No IPv4 address for server");
        }
        public async void Run()
        {
            TcpListener listener = new TcpListener(this.ipAddress, this.port);
            listener.Start();
            Console.Write("Test socket service is now running");
            Console.WriteLine(" " + this.ipAddress + " on port " + this.port);
            Console.WriteLine("Hit <enter> to stop service\n");
            while (true)
            {
                try
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    Task t = Process(tcpClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private async Task Process(TcpClient tcpClient)
        {
            string clientEndPoint = tcpClient.Client.RemoteEndPoint.ToString();
            Console.WriteLine("Received connection request from " + clientEndPoint);
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                StreamReader reader = new StreamReader(networkStream);
                StreamWriter writer = new StreamWriter(networkStream);
                writer.AutoFlush = true;
                while (true)
                {
                    string requestStr = await reader.ReadLineAsync();
                    if (requestStr != null)
                    {

                        Communication request = JsonConvert.DeserializeObject<Communication>(requestStr, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
                        Console.WriteLine("Received service request: " + request);
                        DbServices services = new DbServices();
                        services.requestHandler(request);
                        Communication response = services.getResponse();
                        await writer.WriteLineAsync(JsonConvert.SerializeObject(response,new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto}));
                    }
                    else
                    {
                        Console.WriteLine("Connection closed, client: " + clientEndPoint);
                        break; // Client closed connection
                    }
                }
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                if (tcpClient.Connected)
                {
                    tcpClient.Close();
                }
                Console.WriteLine("Connection closed, client: " + clientEndPoint);
            }
        }
    }
}
