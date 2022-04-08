using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private TimeService _timeService;
    
    [SerializeField] private Transform hourArrow;
    [SerializeField] private Transform minuteArrow;
    [SerializeField] private Transform secondArrow;
    [SerializeField] private Text timeText;

    private Quaternion _hourAngle;
    private Quaternion _minuteAngle;
    private Quaternion _secondAngle;

    public static DateTime Time;
    
    private void Awake()
    {
        _timeService = new TimeService();
    }

    private void Start() // TODO: Сделать считывание времени из разных источников
    {
        Time = _timeService.GetTime();
        timeText.text = Time.ToString("HH:mm:ss");
        Invoke(nameof(OnTimeChanged), 1 - Time.Millisecond * 0.001f);
    }

    private void Update()
    {
        _secondAngle = Quaternion.Euler(0, 0, -1 * (Time.Second + (0.001f * Time.Millisecond)) * 6f);
        _minuteAngle = Quaternion.Euler(0, 0, -1 * (Time.Minute + (Time.Second / 60f)) * 6f);
        _hourAngle = Quaternion.Euler(0, 0, -1 * (Time.Hour * 30f + (Time.Minute * 0.5f)));
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
        timeText.text = Time.ToString("HH:mm:ss");

        ApplicationManager.AlarmClockManager.TimeUpdate();
    }
}
