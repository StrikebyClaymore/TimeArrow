using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetDateSubMenu : SubMenu
{
    [SerializeField] private AlarmClockMenu alarmClockMenu;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Transform dayButtons;
    
    private bool[] _daysOldState;
    
    private void Awake()
    {
        ConnectButtons();
    }

    public override void Activate()
    {
        base.Activate();
        SaveOldState();
    }

    private void SaveOldState()
    {
        _daysOldState = (bool[]) AlarmClockManager.AlarmClock.DaysOn.Clone();
    }

    private void CancelChanges()
    {
        for (int i = 0; i < _daysOldState.Length; i++)
        {
            if(_daysOldState[i] == AlarmClockManager.AlarmClock.DaysOn[i])
                continue;
            var button = dayButtons.GetChild(i).GetComponent<DayButton>();
            button.On(i);
        }
    }
    
    private void Save()
    {
        alarmClockMenu.SetDate();
        Deactivate();
    }

    private void Cancel()
    {
        CancelChanges();
        Deactivate();
    }

    private void ConnectButtons()
    {
        saveButton.onClick.AddListener(Save);
        cancelButton.onClick.AddListener(Cancel);
        
        for (int i = 0; i < dayButtons.childCount; i++)
        {
            var button = dayButtons.GetChild(i).GetComponent<DayButton>();
            button.ConnectOnClick(i);
        }
    }
}
