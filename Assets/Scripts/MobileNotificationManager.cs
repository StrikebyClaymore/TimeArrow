using System;
using UnityEngine;
using Unity.Notifications.Android;

public class MobileNotificationManager : MonoBehaviour
{
    private AndroidNotificationChannel _defaultChannel;
    private AndroidNotification _defaultNotification;
    private int _identifier;
    
    private void Awake()
    {
        _defaultChannel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Description = "",
            Importance = Importance.Default
        };
        
        AndroidNotificationCenter.RegisterNotificationChannel(_defaultChannel);

        var date = AlarmClockManager.AlarmClock.GetTime();
        _defaultNotification = new AndroidNotification()
        {
            Title = "Будильник",
            Text = date.ToString("HH:mm"),
            FireTime = DateTime.Now.AddSeconds(5)//date
        };
        
        _identifier = AndroidNotificationCenter.SendNotification(_defaultNotification, _defaultChannel.Id);

        AndroidNotificationCenter.NotificationReceivedCallback notificationReceivedHandler = delegate(AndroidNotificationIntentData data)
        {
            var msg = "Notification received : " + data.Id + "\n";
            msg +=  "\n Notification received: ";
            msg +=  "\n .Title: " + data.Notification.Title;
            msg +=  "\n .Text: " + data.Notification.Text;
            msg +=  "\n .Channel: " + data.Channel;
            Debug.Log(msg);
        };

        AndroidNotificationCenter.OnNotificationReceived += notificationReceivedHandler;

        var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

        if (notificationIntentData != null)
        {
            Debug.Log("App was opened with notification");
        }
    }
}