using System.Net;
using System.Net.Sockets;

using UnityEngine;

public class IntendixReciever : MonoBehaviour {
    HelicopterController _helicopterController;

    IPEndPoint _endpoint;
    UdpClient _udpClient;

    public string turnLeft;
    public string turnRight;
    public string shoot;

    public GameObject ShootPrefab;

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
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }
    }

    private void HandleCommand(string command)
    {
        Debug.Log("Command: " + command);
        if (command == turnLeft) {
            Debug.Log("Processed Turning left");
            _helicopterController.ManualTurnLeft();
        }
        
        if (command == turnRight) {
            Debug.Log("Processed Turning right");
            _helicopterController.ManualTurnRight();
        }

        if (command == shoot)
        {
            Debug.Log("Processed Shoot");
            Shoot();
        }
    }

    void Shoot()
    {
        var projectile = Instantiate(ShootPrefab, transform.position, transform.rotation);
        projectile.transform.RotateAround(transform.position, transform.up, Random.Range(-10f, 10f));
    }

}
