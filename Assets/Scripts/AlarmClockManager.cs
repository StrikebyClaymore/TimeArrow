using System;
using Extensions;
using UnityEngine;

[RequireComponent(typeof(AlarmPlayer))]
public class AlarmClockManager : MonoBehaviour
{
    public static AlarmClock AlarmClock;
    private AlarmPlayer _alarmPlayer;
    [HideInInspector]
    public int weekOfMonth;
    private DateTime _date;

    private void Awake()
    {
        _alarmPlayer = GetComponent<AlarmPlayer>();
        AlarmClock = new AlarmClock();
        Load();
    }

    public void StartAlarm()
    {
        ApplicationManager.RootMenu.ChangeController(RootMenu.MenuTypeEnum.AlarmMenu);
        _alarmPlayer.Play();
        AlarmClock.CurrentDayAlarmSetPlayed();
    }

    public void StopAlarm()
    {
        _alarmPlayer.Stop();
    }

    public void PostponeAlarm()
    {
        var h = AlarmClock.Minute + 5 > 60 ? 1 : 0;
        AlarmClock.Minute = h == 1 ? (AlarmClock.Minute + 5) % 60 : AlarmClock.Minute + 5;
        AlarmClock.Hour = AlarmClock.Hour + h == 24 ? 0 : AlarmClock.Hour + h; // TODO: Сделать включение будильника в новый день
        ApplicationManager.RootMenu.clockMenu.UpdateAlarmClock();
    }

    public void TimeUpdate()
    {
        CheckNextWeek();
        if (TimeIsAlarmTime())
            StartAlarm();
    }

    private bool TimeIsAlarmTime()
    {
        if (!AlarmClock.On || AlarmClock.CurrentDayAlarmIsPlayed())
            return false;
        var time = Clock.Time;
        var alarmTime = AlarmClock.GetDateTime();
        return time.Hour == alarmTime.Hour && time.Minute == alarmTime.Minute;
    }

    private void CheckNextWeek()
    {
        var systemDate = DateTime.Now;
        var systemWeekOfMonth = systemDate.WeekOfMonth();
        if (_date.Year == systemDate.Year && _date.Month == systemDate.Month && weekOfMonth == systemWeekOfMonth)
            return;
        _date = systemDate;
        weekOfMonth = systemWeekOfMonth;
        AlarmClock.ResetAllDays();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("WeekOfMonth", weekOfMonth);
        PlayerPrefs.SetString("Date", _date.ToString("d"));
    }
    
    private void Load()
    {
        weekOfMonth = PlayerPrefs.GetInt("WeekOfMonth");
        if (weekOfMonth == 0)
            weekOfMonth = DateTime.Now.WeekOfMonth();
        
        var dateString = PlayerPrefs.GetString("Date");
        if (dateString == "")
            dateString = DateTime.Now.ToString("d");
        else
            _date = DateTime.Parse(dateString);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
