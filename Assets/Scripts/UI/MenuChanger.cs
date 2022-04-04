using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChanger : MonoBehaviour
{
    [Header("Portrait")]
    [SerializeField] private ClockMenu clockMenuP;
    [SerializeField] private AlarmClockMenu alarmClockMenuP;
    [Header("Landscape")]
    [SerializeField] private ClockMenu clockMenuL;
    [SerializeField] private AlarmClockMenu alarmClockMenuL;

    public void Change(DeviceOrientation orientation, ClockMenu cm, AlarmClockMenu acm)
    {
        switch (orientation)
        {
            case DeviceOrientation.Portrait:
                cm = clockMenuP;
                acm = alarmClockMenuP;
                break;
            default:
                cm = clockMenuL;
                acm = alarmClockMenuL;
                break;
        }
    }
}
