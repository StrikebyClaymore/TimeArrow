using System;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class ClockMenuView: UIView
{
    [Header("Buttons")]
    [SerializeField] private Button addAlarmClockButton;
    [SerializeField] private Button setAlarmClockButton;
    [SerializeField] private AlarmClockButton alarmClockButton;
    
    [Header("Text")]
    [SerializeField] private Text alarmTimeText;
    [SerializeField] private DaysText daysText;
    
    [Header("")]
    [SerializeField] private GameObject alarmClockContainer;

    [Header("Actions")]
    public Action OnAddAlarmClock;
    public Action OnAlarm;

    private void Start()
    {
        addAlarmClockButton.onClick.AddListener(AddAlarmClockClick);
        setAlarmClockButton.onClick.AddListener(AddAlarmClockClick);
        alarmClockButton.ConnectOnClick(OnAlarm);
    }

    public void AlarmButtonChangeState()
    {
        alarmClockButton.ChangeState();
    } 
    
    public void OpenAlarmClockContainer()
    {
        if(alarmClockContainer.activeSelf)
            return;
        
        AlarmClockManager.AlarmClock.Created = true;
        AlarmClockManager.AlarmClock.Enable();
        
        alarmClockContainer.SetActive(true);
        addAlarmClockButton.gameObject.SetActive(false);

        alarmClockButton.ChangeState();
    }

    public void UpdateAlarmClockValues()
    {
        alarmTimeText.text = new TimeSpan().FormatToAlarmTime();
        daysText.UpdateText();
    }
    
    private void AddAlarmClockClick() => OnAddAlarmClock?.Invoke();

}
