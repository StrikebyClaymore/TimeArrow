using System.Collections;
using UnityEngine;

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

    public enum SetTimeType
    {
        Hour,
        Minute,
        Init,
    }

    #endregion
    
    #region Base and main methods for SetTimeSubMenu

    protected override void Awake()  // TODO: Пофиксить неправильное отображение стрелки и кружка у часов. Вроде всё работает корректно, но нужны ещё проверки.
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
        
        InitTime(SetTimeType.Hour);
    }

    public override void ChangeOrientation()
    {
        base.ChangeOrientation();
        InitTime(_type);
    }

    private void InitTime(SetTimeType type)
    {
        var time = 0;
        switch (type)
        {
            case SetTimeType.Minute:
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
    
    private void UpSetTime(SetTimeType type, int time)
    {
        inputIsLocked = true;
        
        SetTime(type, time);

        foreach (var ui in uiArray)
        {
            ui.SelectTimeUpdate(type, time);
        }
        
        if (type == SetTimeType.Hour)
            foreach (var ui in uiArray)
            {
                ui.SetSelectMinutes();
                ui.SelectTimeUpdate(SetTimeType.Minute, _minute);
            }

        StartCoroutine(UnlockInput());
    }

    private IEnumerator UnlockInput()
    {
        yield return new WaitForSeconds(1.0f);
        inputIsLocked = false;
    }
    
    private void OverSelectTime(SetTimeType type, int time)
    {
        if(inputIsLocked)
            return;
        
        SetTime(type, time);
        
        foreach (var ui in uiArray)
        {
            ui.SelectTimeUpdate(type, time);
        }
    }

    private void SetTime(SetTimeType type, int time)
    {
        _type = type;
        switch (type)
        {
            case SetTimeType.Minute:
                _minute = time;
                AlarmClockManager.AlarmClock.Minute = _minute;
                break;
            default:
                _hour = time;
                AlarmClockManager.AlarmClock.Hour = _hour;
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
        mobileKeyboardInput.OpenKeyboard();
    }

    private void OpenSetMinute()
    {
        SetSelectMinutes();
        mobileKeyboardInput.OpenKeyboard();
    }

    private void SetInputTime(int hour, int minute)
    {
        OverSelectTime(SetTimeType.Hour, hour);
        OverSelectTime(SetTimeType.Minute, minute);
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