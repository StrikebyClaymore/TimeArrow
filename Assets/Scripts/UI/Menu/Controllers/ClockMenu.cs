﻿using System;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class ClockMenu : BaseMenuController<ClockMenuView>
{
    protected override void Awake()
    {
        base.Awake();
        ConnectActions();
    }

    public void SetAlarmClock()
    {
        UpdateAlarmClock();
        
        foreach (var ui in uiArray)
        {
            ui.OpenAlarmClockContainer();
        }
    }

    private void UpdateAlarmClock()
    {
        AlarmClockManager.AlarmClock.ResetCurrentDay();
        foreach (var ui in uiArray)
        {
            ui.UpdateAlarmClockValues();
        }
    }

    private void AlarmChangeState()
    {
        AlarmClockManager.AlarmClock.Enable();
        foreach (var ui in uiArray)
        {
            ui.AlarmButtonChangeState();
        }
    }
    
    private void OpenAlarmClockMenu()
    {
        root.ChangeController(RootMenu.MenuTypeEnum.AlarmClockMenu);
    }
    
    private void ConnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnAddAlarmClock += OpenAlarmClockMenu;
            ui.OnAlarm += AlarmChangeState;
        }
    }
    
    private void DisconnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnAddAlarmClock -= OpenAlarmClockMenu;
            ui.OnAlarm -= AlarmChangeState;
        }
    }
}
