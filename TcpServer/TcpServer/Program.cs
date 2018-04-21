using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

public class TcpServer
{

    static void Main(string[] args)
    {
        Int32 port = 5678;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        _server = new TcpListener(localAddr, port);
        _server.Start();
        Console.WriteLine("Waiting for a connection... ");
        _client = _server.AcceptTcpClient();

        while (_client.Connected)
        {
            Update();
        }

        Console.WriteLine("Client not connected");
    }

    static void Update()
    {
        if (!_client.Connected)
        {
            return;
        }

        Console.WriteLine("Client connected");

        try
        {
            NetworkStream stream = _client.GetStream();
            ReadDataFromClient(stream);
            string responseMessage = "Message recieved";
            ResponseToClient(stream, responseMessage);
            _client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            Console.WriteLine("FINISHED");
            _server.Stop();
        }
    }

    static TcpClient _client;
    static TcpListener _server = null;
    static Byte[] _buffor = new Byte[256];

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

    static void ResponseToClient(NetworkStream clientStream, string message)
    {
        var rawResponse = System.Text.Encoding.ASCII.GetBytes(message);
        clientStream.Write(rawResponse, 0, rawResponse.Length);
        Console.WriteLine("Sent: {0}", message);
        _client.Close();
    }
}
