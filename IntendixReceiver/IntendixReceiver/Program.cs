using Extendix.ItemReceiver;
using Intendix.Board;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace IntendixTransmitter
{
    public class CommandTransmitter : ItemReceiver
    {
        UdpClient _receiverSocket;

        public CommandTransmitter() {
            _receiverSocket = new UdpClient();
            _receiverSocket.Connect("127.0.0.1", 1001);
        }

        public override void ItemReceived(BoardItem item)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(item.Name);
            _receiverSocket.Send(buffer, buffer.Length);
            Console.WriteLine("Transmitted " + item.Name);
        }
    }

    static class Program
    {
        static void Main() {
            var transmitter = new CommandTransmitter();
            transmitter.BeginReceiving(IPAddress.Parse("127.0.0.1"), 1000);

            Console.ReadKey();
        }
    }
}

