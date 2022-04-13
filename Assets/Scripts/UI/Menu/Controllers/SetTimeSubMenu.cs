using System;
using UnityEngine;
using UnityEngine.UI;

public class SetTimeSubMenu : SubMenuController<SetTimeView, AlarmClockMenu>
{
    #region Variables

    [SerializeField] private MobileKeyboardInput mobileKeyboardInput;
    
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

    #endregion
    
    #region Base and main methods for SetTimeSubMenu

    protected override void Awake()  // TODO: Сделать чтобы можно было указывать время числами
    {
        base.Awake();
        ConnectActions();
    }

    private void Start()
    {
        mobileKeyboardInput.Init(SetInputTime);
        
        foreach (var ui in uiArray)
        {
            ui.Init();
        }
    }

    public override void Activate()
    {
        base.Activate();
        SaveOldTime();
        
        InitTime(SetTimeTypeEnum.Hour);
    }

    public override void ChangeOrientation()
    {
        base.ChangeOrientation();
        InitTime(_type);
    }

    private void InitTime(SetTimeTypeEnum type)
    {
        var time = 0;
        switch (type)
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
            ui.SelectTimeUpdate(type, time);
        }
    }

    private void Save()
    {
        rootController.SetTime();
        Close();
    }

    private void Cancel()
    {
        CancelTime();
        Close();
    }

    private void Close()
    {
        rootController.currentSubMenu = null;
        Deactivate();
        foreach (var ui in uiArray)
        {
            ui.Reset();
        }
    }

    #endregion
    
    #region Base select time

    private void SaveOldTime()
    {
        _oldHour = AlarmClockManager.AlarmClock.Hour;
        _oldMinute = AlarmClockManager.AlarmClock.Minute;
    }

    private void CancelTime()
    {
        _hour = _oldHour;
        _minute = _oldMinute;
        AlarmClockManager.AlarmClock.Hour = _oldHour; 
        AlarmClockManager.AlarmClock.Minute = _oldMinute;
    }
    
    private void SetTime(SetTimeTypeEnum type, int time)
    {
        foreach (var ui in uiArray)
        {
            ui.TimeUpdate(type, time);
        }
    }

    private void SelectTime(SetTimeTypeEnum type, int time)
    {
        _type = type;
        switch (type)
        {
            case SetTimeTypeEnum.Minute:
                _minute = time;
                AlarmClockManager.AlarmClock.Minute = _minute;
                break;
            default:
                _hour = time;
                AlarmClockManager.AlarmClock.Hour = _hour;
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

    #endregion
    
    #region Numbers input time

    private void OpenSetHour()
    {
        SetSelectHours();
        mobileKeyboardInput.OpenKeyboard();
    }

    private void OpenSetMinute()
    {
        SetSelectMinutes();
        mobileKeyboardInput.OpenKeyboard();
    }

    private void SetInputTime(int hour, int minute)
    {
        SelectTime(SetTimeTypeEnum.Hour, hour);
        SelectTime(SetTimeTypeEnum.Minute, minute);
    }

    #endregion

    #region Connect Actions

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
            ui.OnSetHours += OpenSetHour;
            ui.OnSetMinutes += OpenSetMinute;
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
            ui.OnSetHours -= OpenSetHour;
            ui.OnSetMinutes -= OpenSetMinute;
        }
    }
    
    #endregion
}