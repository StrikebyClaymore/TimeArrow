 using System;
 using System.Globalization;
 using System.Net;
 using System.Net.Sockets;
 using UnityEngine;

 public class TimeService
 {
     private const string Address1 = "https://www.google.com";
     private const string Address2 = "https://www.microsoft.com";

     public DateTime GetTime()
     {
         if (!CheckInternetConnection())
             return DateTime.Now;

         var time1 = GetNetworkTime(Address1);
         var time2 = GetNetworkTime(Address2);

         var ms = (time1.Millisecond + time2.Millisecond) / 2;
         var s = (time1.Second + time2.Second) / 2;
         var m = (time1.Minute + time2.Minute) / 2;
         var h = (time1.Hour + time2.Hour) / 2;

         var time = new DateTime(time1.Year, time1.Month, time1.Day, h, m, s, ms);

         return time;
     }

     private DateTime GetNetworkTime(string address)
     {

         var request = WebRequest.Create(address);
         var response = (HttpWebResponse) request.GetResponse();
         if (response.StatusCode == HttpStatusCode.OK)
         {
             var date = DateTime.ParseExact(response.Headers["date"],
                 "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'",
                 CultureInfo.InvariantCulture.DateTimeFormat,
                 DateTimeStyles.AssumeUniversal);
             response.Close();
             return date;
         }
         else
         {
             response.Close();
             return DateTime.Now;
         }
     }

     private bool CheckInternetConnection()
     {
         return Application.internetReachability != NetworkReachability.NotReachable;
     }
 }