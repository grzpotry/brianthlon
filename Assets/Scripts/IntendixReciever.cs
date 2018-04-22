using System.Net;
using System.Net.Sockets;

using UnityEngine;

public class IntendixReciever : MonoBehaviour {
    HelicopterController _helicopterController;

    IPEndPoint _endpoint;
    UdpClient _udpClient;

    public string turnLeft;
    public string turnRight;

    private void Start() {
        _helicopterController = GetComponent<HelicopterController>();

        _endpoint = new IPEndPoint(IPAddress.Any, 1001);
        _udpClient = new UdpClient(_endpoint);
    }

    private void Update()
    {
        while (_udpClient.Available > 0) {
            byte[] buffer = _udpClient.Receive(ref _endpoint);

            string command = System.Text.Encoding.Default.GetString(buffer);
            Debug.Log(command);
            HandleCommand(command);
            //Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
            //newsock.Send(data, data.Length, sender);
        }

        
    }

    private void HandleCommand(string command)
    {
        if (command == turnLeft) {
            Debug.Log("Turning left");
            _helicopterController.ManualTurnLeft();
        }
        
        if (command == turnRight) {
            Debug.Log("Turning right");
            _helicopterController.ManualTurnRight();
        }
    }
}
