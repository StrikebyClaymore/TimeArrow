using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeSelector : MonoBehaviour
{
    //[SerializeField] private Camera cam;
    [SerializeField] private GameObject hoursClock;
    [SerializeField] private GameObject minutesClock;
    [SerializeField] private Transform hoursContainer;
    [SerializeField] private Transform minutesContainer;
    
    [SerializeField] private Transform circle;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Text hourText;
    [SerializeField] private Text minuteText;
    [SerializeField] private Button hoursButton;
    [SerializeField] private Button minutesButton;
    private int _hour;
    private int _minute;
    public SetTimeEvent SelectedTime;
    
    public enum SetTimeTypeEnum
    {
        Hour,
        Minute,
        Init,
    }
    
    private void Awake()
    {
        SelectedTime.AddListener(SelectTime);
        minutesClock.SetActive(false);
        hoursClock.SetActive(true);
        hoursButton.onClick.AddListener(SetSelectHours);
        minutesButton.onClick.AddListener(SetSelectMinutes);
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        minutesClock.SetActive(false);
        hoursClock.SetActive(true);
        /*_hour = 12;
        _minute = 0;*/
        SelectTime(hoursContainer.GetChild(_hour).position, _hour);
    }

    private void SetSelectHours()
    {
        minutesClock.SetActive(false);
        hoursClock.SetActive(true);
    }
    
    private void SetSelectMinutes()
    {
        minutesClock.SetActive(true);
        hoursClock.SetActive(false);
    }
    
    private void SelectTime(Vector3 position, int time, SetTimeTypeEnum type = SetTimeTypeEnum.Init)
    {
        circle.position = position;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, position);

        TimeSpan timeSpan;
        switch (type)
        {
            case SetTimeTypeEnum.Minute:
                _minute = time;
                timeSpan = TimeSpan.FromMinutes(time);
                minuteText.text = timeSpan.ToString("mm");
                AlarmClockManager.AlarmClock.Minute = _minute;
                break;
            default:
                _hour = time;
                timeSpan = TimeSpan.FromHours(time);
                hourText.text = timeSpan.ToString("hh");
                AlarmClockManager.AlarmClock.Hour = _hour;
                break;
        }
    }

    public void SetTime(SetTimeTypeEnum type)
    {
        switch (type)
        {
            case SetTimeTypeEnum.Hour:
                hoursClock.SetActive(false);
                minutesClock.SetActive(true);
                SelectTime(minutesContainer.GetChild(_minute).position, _minute, SetTimeTypeEnum.Minute);
                break;
            case SetTimeTypeEnum.Minute:
                break;
            default:
                break;
        }
    }
}
