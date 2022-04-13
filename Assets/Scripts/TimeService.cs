 using System;
 using System.Net;
 using System.Net.Sockets;
 using UnityEngine;

 public class TimeService
 {
     private const string NtpServer1 = "time.windows.com";
     private const string NtpServer2 = "nist.netservicesgroup.com";
     private const int SocketReceiveTimeout = 6000;
     
     public DateTime GetNetworkTime()
     {
         if (!CheckInternetConnection())
             return DateTime.Now;

         var time1 = GetNetworkTime(NtpServer1);
         var time2 = GetNetworkTime(NtpServer2);

         var ms = int.Parse(((time1.Millisecond + time2.Millisecond) / 2).ToString().Substring(0, 3));
         var s = (time1.Second + time2.Second) / 2;
         var m = (time1.Minute + time2.Minute) / 2;
         var h = (time1.Hour + time2.Hour) / 2;
         
         var time = new DateTime(time1.Year, time1.Month, time1.Day, h, m, s, ms);

         return time;
     }

     // stackoverflow.com/a/3294698/162671
     private DateTime GetNetworkTime(string ntpServer)
     {
         var ntpData = new byte[48];
         
         ntpData[0] = 0x1B;

         var addresses = Dns.GetHostEntry(ntpServer).AddressList;
         
         var ipEndPoint = new IPEndPoint(addresses[0], 123);

         using(var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
         {
             socket.Connect(ipEndPoint);
             
             socket.ReceiveTimeout = SocketReceiveTimeout;     

             socket.Send(ntpData);
             socket.Receive(ntpData);
             socket.Close();
         }
         
         const byte serverReplyTime = 40;
         
         ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
         
         ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
         
         intPart = SwapEndianness(intPart);
         fractPart = SwapEndianness(fractPart);

         var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
         
         var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

         return networkDateTime.ToLocalTime();
     }
     
     private uint SwapEndianness(ulong x)
     {
         return (uint) (((x & 0x000000ff) << 24) +
                        ((x & 0x0000ff00) << 8) +
                        ((x & 0x00ff0000) >> 8) +
                        ((x & 0xff000000) >> 24));
     }

     private bool CheckInternetConnection()
     {
         return Application.internetReachability != NetworkReachability.NotReachable;
     }
 }