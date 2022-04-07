using System;
using UnityEngine;
using UnityEngine.UI;

public class SetTimeSubMenu : SubMenuController<SetTimeView, AlarmClockMenu>
{
    private int _hour;
    private int _minute;
    private SetTimeTypeEnum _type;
    
    private int _oldHour;
    private int _oldMinute;

    public enum SetTimeTypeEnum
    {
        Hour,
        Minute,
        Init,
    }
    
    protected override void Awake()
    {
        base.Awake();
        ConnectActions();
    }

    private void Start()
    {
        foreach (var ui in uiArray)
            ui.Init();
    }

    public override void Activate()
    {
        base.Activate();
        SaveOldTime();
        InitTime();
        //timeSelector.Reset();
    }

    private void InitTime()
    {
        Debug.Log("INIT");
        var time = 0;
        switch (_type)
        {
            case SetTimeTypeEnum.Minute:
                time = _minute;
                break;
            default:
                time = _hour;
                break;
        }
        foreach (var ui in uiArray)
        {
            ui.SelectTimeUpdate(_type, time);
        }
    }
    
    private void SaveOldTime()
    {
        _oldHour = AlarmClockManager.AlarmClock.Hour;
        _oldMinute = AlarmClockManager.AlarmClock.Minute;
    }

    private void CancelTime()
    {
        AlarmClockManager.AlarmClock.Hour = _oldHour; 
        AlarmClockManager.AlarmClock.Minute = _oldMinute;
    }
    
    private void Save()
    {
        rootController.SetTime();
        rootController.currentSubMenu = null;
        Deactivate();
    }

    private void Cancel()
    {
        CancelTime();
        rootController.currentSubMenu = null;
        Deactivate();
    }

    private void SetTime(SetTimeTypeEnum type, int time)
    {
        /*foreach (var ui in uiArray)
        {
            ui.TimeUpdate(type, time);
        }*/
    }

    private void SelectTime(SetTimeTypeEnum type, int time)
    {
        _type = type;
        switch (type)
        {
            case SetTimeTypeEnum.Minute:
                _minute = time;
                break;
            default:
                _hour = time;
                break;
        }
        
        foreach (var ui in uiArray)
        {
            ui.SelectTimeUpdate(type, time);
        }
    }

    private void SetSelectHours()
    {
        foreach (var ui in uiArray)
        {
            ui.SetSelectHours();
        }
    }
    
    private void SetSelectMinutes()
    {
        foreach (var ui in uiArray)
        {
            ui.SetSelectMinutes();
        }
    }
    
    private void ConnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnSave += Save;
            ui.OnCancel += Cancel;
            ui.OnSetTime += SetTime;
            ui.OnSelectTime += SelectTime;
            ui.OnSelectHours += SetSelectHours;
            ui.OnSelectMinutes += SetSelectMinutes;
        }
    }
    
    private void DisconnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnSave -= Save;
            ui.OnCancel -= Cancel;
            ui.OnSetTime -= SetTime;
            ui.OnSelectTime -= SelectTime;
            ui.OnSelectHours -= SetSelectHours;
            ui.OnSelectMinutes -= SetSelectMinutes;
        }
    }
}