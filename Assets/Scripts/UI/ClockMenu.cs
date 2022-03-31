using System;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class ClockMenu : BaseMenu
{
    [SerializeField] private AlarmSubMenu alarmSubMenu;
    [SerializeField] private Button addAlarmClockButton;
    [SerializeField] private GameObject alarmClockContainer;
    [SerializeField] private Button setAlarmClockButton;
    [SerializeField] private AlarmClockButton alarmClockButton;
    [SerializeField] private Text timeText;
    [SerializeField] private DaysText daysText;
    
    private void Awake()
    {
        addAlarmClockButton.onClick.AddListener(OpenAlarmClockMenu);
        setAlarmClockButton.onClick.AddListener(OpenAlarmClockMenu);
    }

    public void SetAlarmClock()
    {
        UpdateAlarmClock();
        
        if(alarmClockContainer.activeSelf)
            return;
        
        AlarmClockManager.AlarmClock.Created = true;
        AlarmClockManager.AlarmClock.Enable();
        
        alarmClockContainer.SetActive(true);
        addAlarmClockButton.gameObject.SetActive(false);

        if (!AlarmClockManager.AlarmClock.On) return;
        
        alarmClockButton.On();
    }

    public void OpenAlarmSubMenu()
    {
        alarmSubMenu.Activate();
    }
    
    private void OpenAlarmClockMenu()
    {
        root.ChangeController(RootMenu.MenuTypeEnum.AlarmClockMenu);
    }

    public void UpdateAlarmClock()
    {
        AlarmClockManager.AlarmClock.ResetCurrentDay();
        timeText.text = new TimeSpan().FormatToAlarmTime();
        daysText.UpdateText();
    }
}
