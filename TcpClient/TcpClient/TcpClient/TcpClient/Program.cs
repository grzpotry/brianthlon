using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


class Program
{
    const int PORT_NO = 5679;
    const string SERVER_IP = "127.0.0.1";
    static void Main(string[] args)
    {
        //---data to send to the server---
        string textToSend = DateTime.Now.ToString();

        //---create a TCPClient object at the IP and port no.---
        TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
        NetworkStream nwStream = client.GetStream();

        ReadDataFromClient(nwStream);
        Console.WriteLine("FINISH");
        Console.ReadLine();
        client.Close();
    }

    static void ReadDataFromClient(NetworkStream clientStream)
    {
        String data = null;
        int i;
        while ((i = clientStream.Read(_buffor, 0, _buffor.Length)) != 0)
        {
            data = System.Text.Encoding.ASCII.GetString(_buffor, 0, i);
            Console.WriteLine("Received: {0}", data);
        }
    }

    static Byte[] _buffor = new Byte[256];
}
