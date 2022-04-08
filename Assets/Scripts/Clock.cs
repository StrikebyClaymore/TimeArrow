using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform hourArrow;
    [SerializeField] private Transform minuteArrow;
    [SerializeField] private Transform secondArrow;
    
    [SerializeField] private Text timeText;

    private Quaternion _hourAngle;
    private Quaternion _minuteAngle;
    private Quaternion _secondAngle;

    private void Start()  // TODO: Сделать считывание времени из разных источников
    {
        timeText.text = DateTime.Now.ToString("HH:mm:ss");
        Invoke(nameof(OnTimeChanged), 1 - DateTime.Now.Millisecond * 0.001f);
    }

    private void Update()
    {
        var systemTime = DateTime.Now;
        _secondAngle = Quaternion.Euler(0, 0, -1 * (systemTime.Second + (0.001f * systemTime.Millisecond)) * 6f);
        _minuteAngle = Quaternion.Euler(0, 0, -1 * (systemTime.Minute + (systemTime.Second / 60f)) * 6f);
        _hourAngle = Quaternion.Euler(0, 0, -1 * (systemTime.Hour * 30f + (systemTime.Minute * 0.5f)));
    }

    private void FixedUpdate()
    {
        secondArrow.rotation = _secondAngle;
        minuteArrow.rotation = _minuteAngle;
        hourArrow.rotation = _hourAngle;
    }
    
    private void OnTimeChanged()
    {
        Invoke(nameof(OnTimeChanged), 1 - DateTime.Now.Millisecond * 0.001f);
        timeText.text = DateTime.Now.ToString("HH:mm:ss");

        ApplicationManager.AlarmClockManager.TimeUpdate();
    }
}
