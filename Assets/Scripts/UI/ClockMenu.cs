using System;
using UnityEngine;
using UnityEngine.UI;

public class ClockMenu : BaseMenu
{
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
        alarmClockButton.On();
        alarmClockContainer.SetActive(true);
        addAlarmClockButton.gameObject.SetActive(false);
    }

    private void OpenAlarmClockMenu()
    {
        root.ChangeController(RootMenu.MenuTypeEnum.AlarmClockMenu);
    }

    private void UpdateAlarmClock()
    {
        timeText.text = ApplicationManager.AlarmClockManager.FormatTime();
        daysText.UpdateText();
    }
}
