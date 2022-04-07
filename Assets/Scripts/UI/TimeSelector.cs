using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeSelector : MonoBehaviour
{
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

    public int _hour;
    public int _minute;
    public SetTimeEvent TimeSelected;
    
    public enum SetTimeTypeEnum
    {
        Hour,
        Minute,
        Init,
    }

    public SetTimeView rootUI;
    
    private void Awake()
    {
        //TimeSelected.AddListener(SelectTime);
        /*minutesClock.SetActive(false);
        hoursClock.SetActive(true);*/
        //hoursButton.onClick.AddListener(SetSelectHours);
        //minutesButton.onClick.AddListener(SetSelectMinutes);
    }

    private void Start()
    {
        //Reset();
    }
    
    public void Reset()
    {
        /*minutesClock.SetActive(false);
        hoursClock.SetActive(true);*/
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
    
    public void SelectTime(Vector3 position, int time, SetTimeSubMenu.SetTimeTypeEnum type = SetTimeSubMenu.SetTimeTypeEnum.Init)
    {
        circle.position = position;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, position);

        TimeSpan timeSpan;
        switch (type)
        {
            case SetTimeSubMenu.SetTimeTypeEnum.Minute:
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

    public void SetTime(SetTimeSubMenu.SetTimeTypeEnum type)
    {
        switch (type)
        {
            case SetTimeSubMenu.SetTimeTypeEnum.Hour:
                hoursClock.SetActive(false);
                minutesClock.SetActive(true);

                SelectTime(minutesContainer.GetChild(_minute).position, _minute, SetTimeSubMenu.SetTimeTypeEnum.Minute);
                
                var timeSpan = TimeSpan.FromHours(_hour);
                hourText.text = timeSpan.ToString("hh");
                Debug.Log("SET HOUR");
                break;
            case SetTimeSubMenu.SetTimeTypeEnum.Minute:
                break;
            default:
                break;
        }
    }
}
