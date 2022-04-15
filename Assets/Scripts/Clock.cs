using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : BaseMenuController<ClockView>
{
    public static DateTime ClockTime;

    private TimeService _timeService;

    private UpdateTimer _checkGlobalTimeTimer;
    private const float MAXTime = 3600f;
    private const string TimeLeftSaveName = "TimeLeft";

    private Quaternion _hourAngle;
    private Quaternion _minuteAngle;
    private Quaternion _secondAngle;
    
    protected override void Awake()
    {
        base.Awake();
        _timeService = new TimeService();
    }
    
    private void Start()
    {
        _checkGlobalTimeTimer = gameObject.AddComponent<UpdateTimer>();
        _checkGlobalTimeTimer.Init(MAXTime, true, CheckGlobalTime, LoadTimeLeft());

        ClockTime = _timeService.GetNetworkTime();

        foreach (var ui in uiArray)
        {
            ui.Init(ClockTime);
        }
        
        
        Invoke(nameof(OnTimeChanged), 1 - ClockTime.Millisecond * 0.001f);
        
        Invoke(nameof(FixTime), 1f);
    }

    private void Update()
    {
        ClockTime = ClockTime.AddSeconds(Time.deltaTime);
        _secondAngle = Quaternion.Euler(0, 0, -1 * (ClockTime.Second + (0.001f * ClockTime.Millisecond)) * 6f);
        _minuteAngle = Quaternion.Euler(0, 0, -1 * (ClockTime.Minute + (ClockTime.Second / 60f)) * 6f);
        _hourAngle = Quaternion.Euler(0, 0, -1 * (ClockTime.Hour * 30f + (ClockTime.Minute * 0.5f)));
    }

    private void FixedUpdate()
    {
        foreach (var ui in uiArray)
        {
            ui.RotateArrows(_hourAngle, _minuteAngle, _secondAngle);    
        }
    }

    private void CheckGlobalTime()
    {
        ClockTime = _timeService.GetNetworkTime();
    }

    private void FixTime()
    {
        ClockTime = _timeService.GetNetworkTime();
    }
    
    private void OnTimeChanged()
    {
        Invoke(nameof(OnTimeChanged), 1 - DateTime.Now.Millisecond * 0.001f);

        foreach (var ui in uiArray)
        {
            ui.UpdateText(ClockTime);
        }

        ApplicationManager.AlarmClockManager.TimeUpdate();
    }

    private float LoadTimeLeft()
    {
        return PlayerPrefs.GetFloat(TimeLeftSaveName);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(TimeLeftSaveName, _checkGlobalTimeTimer.timeLeft);
    }
}
