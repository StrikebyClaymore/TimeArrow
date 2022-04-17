using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetTimeSubMenu : SubMenuController<SetTimeView, AlarmClockMenu>
{
    #region Variables

    [SerializeField] private MobileKeyboardInput mobileKeyboardInput;

    [HideInInspector]
    public bool inputIsLocked; 
    
    private int _hour;
    private int _minute;
    private SetTimeType _type;
    
    private int _oldHour;
    private int _oldMinute;

    private ClockTimeButton _selectedTimeButton;
    
    public enum SetTimeType
    {
        Hour,
        Minute,
        Init,
    }

    #endregion
    
    #region Base and main methods for SetTimeSubMenu

    protected override void Awake()
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
        foreach (var ui in uiArray)
        {
            ui.SelectTimeUpdate(_type, _hour);   
        }
    }

    public override void ChangeOrientation()
    {
        inputIsLocked = true;
        base.ChangeOrientation();
        foreach (var ui in uiArray)
        {
            ui.SelectTimeUpdate(_type, _type == SetTimeType.Hour ? _hour : _minute);
        }
        StartCoroutine(UnlockInput());
    }

    private void Save()
    {
        AlarmClockManager.AlarmClock.NewMinute = _minute;
        AlarmClockManager.AlarmClock.NewHour = _hour;
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
        _type = SetTimeType.Hour;
        _selectedTimeButton = null;
        rootController.currentSubMenu = null;
        foreach (var ui in uiArray)
        {
            ui.Reset();
        }
        Deactivate();
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
        AlarmClockManager.AlarmClock.NewHour = _hour;
        AlarmClockManager.AlarmClock.NewMinute = _minute;

        foreach (var ui in uiArray)
        {
            ui.SelectTimeUpdate(SetTimeType.Minute, _minute);
            ui.SelectTimeUpdate(SetTimeType.Minute, _hour);
        }
    }
    
    /// <summary>
    /// Check TimeButton Up, If Up - update time
    /// </summary>
    private void Update()
    {
        if(_selectedTimeButton is null)
            return;
        if (_selectedTimeButton.selected && Input.GetMouseButtonUp(0))
            UpSetTime(_selectedTimeButton);
    }

    private void UpSetTime(ClockTimeButton button)
    {
        inputIsLocked = true;

        var type = button.type;
        var time = button.time;

        SetTime(type, time);

        if (type == SetTimeType.Hour)
        {
            _type = SetTimeType.Minute;
            foreach (var ui in uiArray)
            {
                ui.SelectTimeUpdate(type, time);
                ui.SetSelectMinutes();
                ui.SelectTimeUpdate(_type, _minute);
            }
            
            StartCoroutine(UnlockInput());
        }
        else
        {
            inputIsLocked = false;

            foreach (var ui in uiArray)
            {
                ui.SelectTimeUpdate(type, time);
            }
        }

        _selectedTimeButton = null;
    }

    private IEnumerator UnlockInput()
    {
        yield return new WaitForSeconds(1.0f);
        inputIsLocked = false;
    }
    
    private void OverSelectTime(ClockTimeButton button)
    {
        if(inputIsLocked)
            return;
        
        _selectedTimeButton = button;
        var type = button.type;
        var time = button.time;

        SetTime(type, time);
        
        foreach (var ui in uiArray)
        {
            ui.SelectTimeUpdate(type, time);
        }
    }

    private void SetTime(SetTimeType type, int time)
    {
        switch (type)
        {
            case SetTimeType.Minute:
                _minute = time;
                break;
            default:
                _hour = time;
                break;
        }
    }

    /// <summary>
    /// Show Hour clock
    /// </summary>
    private void SetSelectHours()
    {
        _type = SetTimeType.Hour;
        foreach (var ui in uiArray)
        {
            ui.SetSelectHours();
            ui.SelectTimeUpdate(_type, _hour);
        }
    }
    
    /// <summary>
    /// Show Minute clock
    /// </summary>
    private void SetSelectMinutes()
    {
        _type = SetTimeType.Minute;
        foreach (var ui in uiArray)
        {
            ui.SetSelectMinutes();
            ui.SelectTimeUpdate(_type, _minute);
        }
    }

    #endregion
    
    #region Numbers input time

    private void OpenSetHour()
    {
        SetSelectHours();
        mobileKeyboardInput.OpenKeyboard(_hour, _minute);
    }

    private void OpenSetMinute()
    {
        SetSelectMinutes();
        mobileKeyboardInput.OpenKeyboard(_hour, _minute);
    }

    private void SetInputTime(int hour, int minute)
    {
        _hour = hour;
        _minute = minute;
        foreach (var ui in uiArray)
        {
            ui.SetTime(SetTimeType.Hour, _hour);
            ui.SetTime(SetTimeType.Minute, _minute);
            ui.SelectTimeUpdate(_type, _type == SetTimeType.Hour ? _hour : _minute);
        }
    }

    #endregion

    #region Connect Actions

    private void ConnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnSave += Save;
            ui.OnCancel += Cancel;
            
            ui.OnSetTime += UpSetTime;
            ui.OnSelectTime += OverSelectTime;
            
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
            
            ui.OnSetTime -= UpSetTime;
            ui.OnSelectTime -= OverSelectTime;
            
            ui.OnSelectHours -= SetSelectHours;
            ui.OnSelectMinutes -= SetSelectMinutes;
            ui.OnSetHours -= OpenSetHour;
            ui.OnSetMinutes -= OpenSetMinute;
        }
    }
    
    #endregion
}