using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Async_Client
{
    class Client
    {
        private TcpClient tcpClient;

        public async Task Initialize(string ip, int port)
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ip, port);

            Console.WriteLine("Connected to: {0}:{1}", ip, port);
        }

        public async Task Read()
        {
            var buffer = new byte[4096];
            var ns = tcpClient.GetStream();
            while (true)
            {
                var bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) return; // Stream was closed
                Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bytesRead));
            }
        }

        public async Task Send( string message)
        {
            byte[] length = BitConverter.GetBytes(message.Length);
            byte[] buffer = new byte[sizeof(int) + message.Length + 1];
            var ns = tcpClient.GetStream();
            while (true)
            {
                var bytesSend = await ns.ReadAsync(buffer, 0, buffer.Length);
                if (bytesSend == 0) return; // Stream was closed
                Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bytesSend));
            }
            for (int i = 0; i < sizeof(int); i++)
            {
                buffer[i + 1] = length[i];
            }
            for (int i = 0; i < message.Length; i++)
            {
            //    buffer[i + 1 + sizeof(int)] = message[i];
            }

        }


    }
}
