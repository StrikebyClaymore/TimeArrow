using System;
using UnityEngine;
using UnityEngine.UI;

public class AlarmMenuView : UIView
{
    [SerializeField] private Button stopButton;
    [SerializeField] private Button postponeButton;

    public Action OnStopAlarm;
    public Action OnPostponeAlarm;
    
    private void Start()
    {
        stopButton.onClick.AddListener(StopAlarmButtonClick);
        postponeButton.onClick.AddListener(PostponeAlarmButtonClick);
    }
    
    private void StopAlarmButtonClick()
    {
        OnStopAlarm?.Invoke();
    }
    
    private void PostponeAlarmButtonClick()
    {
        OnPostponeAlarm?.Invoke();
    }
}