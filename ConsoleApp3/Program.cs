using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketDotNet;
using SharpPcap;
using SharpPcap.WinDivert;


namespace ConsoleApp3
{
    internal class Program
    {
        static WinDivertDevice Device;
        static WinDivertHeader _lastCaptureHeader;

        static void Main(string[] args)
        {
            Device = new SharpPcap.WinDivert.WinDivertDevice { Flags = 0 };
            Device.OnPacketArrival += Device_OnPacketArrival;

        }

        private static void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            var rawPacket = e.GetPacket();
            var packet = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);
            var ipPacket = packet.Extract<IPPacket>();

            //ipPacket.Protocol == PacketDotNet.ProtocolType.Tcp

            _lastCaptureHeader = (WinDivertHeader)e.Header;

            ProcessPacketReceivedFromInbound(ipPacket);
        }
    }
}
