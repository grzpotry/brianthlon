using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TcpListenerService: MonoBehaviour
{
    protected void Start()
    {
        Int32 port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        _server = new TcpListener(localAddr, port);
        _server.Start();
        Debug.Log("Waiting for a connection... ");
        _client = _server.AcceptTcpClient();
    }

    protected void Update()
    {
        if (!_client.Connected)
        {
            return;
        }

        Debug.Log("Client connected");

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
            Debug.LogException(ex);
        }
        finally
        {
            Debug.Log("FINISHED");
            _server.Stop();
        }
    }

    TcpClient _client;
    TcpListener _server = null;
    Byte[] _buffor = new Byte[256];

    void ReadDataFromClient(NetworkStream clientStream)
    {
        String data = null;
        int i;
        while ((i = clientStream.Read(_buffor, 0, _buffor.Length)) != 0)
        {
            data = System.Text.Encoding.ASCII.GetString(_buffor, 0, i);
            Debug.LogFormat("Received: {0}", data);
        }
    }

    void ResponseToClient(NetworkStream clientStream, string message)
    {
        var rawResponse = System.Text.Encoding.ASCII.GetBytes(message);
        clientStream.Write(rawResponse, 0, rawResponse.Length);
        Debug.LogFormat("Sent: {0}", message);
        _client.Close();
    }
}
