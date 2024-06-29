using System;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using PacketDotNet;
using SharpPcap;
namespace test_rec
{
    class Program
    {

        public void Device_OnPacketArrival(object s, PacketCapture e)
        {
            Console.WriteLine(e.GetPacket());
        }
        public static void sendPacket()
        {
            // Print SharpPcap version
            var ver = Pcap.SharpPcapVersion;
            Console.WriteLine("SharpPcap {0}, Example9.SendPacket.cs\n", ver);

            // Retrieve the device list
            var devices = CaptureDeviceList.Instance;

            // If no devices were found print an error
            if (devices.Count < 1)
            {
                Console.WriteLine("No devices were found on this machine");
                return;
            }

            Console.WriteLine("The following devices are available on this machine:");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();

            int i = 0;

            // Print out the available devices
            foreach (var dev in devices)
            {
                Console.WriteLine("{0}) {1}", i, dev.Description);
                i++;
            }
            

            using var device = devices[5];


            //Open the device
            device.Open();

            //Generate a random packet
            byte[] bytes = GetRandomPacket();

            while (true)
            {
                try
                {
                    //Send the packet out the network device
                    device.SendPacket(bytes);
                    Console.WriteLine("-- Packet sent successfuly.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("-- " + e.Message);
                }
            }
        }


        public static void Main()
        {

            Thread send_thread_object = new Thread(() => { sendPacket(); });
            send_thread_object.Start();


            Console.Write("Hit 'Enter' to exit...");
            Console.ReadLine();
        }


        
        /// <summary>
        /// Generates a random packet of size 200
        /// </summary>
        private static byte[] GetRandomPacket()
        {
            byte[] packet = new byte[6232];
            Random rand = new Random();
            rand.NextBytes(packet);
            return packet;
        }
    }

}


