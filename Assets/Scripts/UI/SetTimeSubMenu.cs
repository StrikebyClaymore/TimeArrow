using UnityEngine;
using UnityEngine.UI;

public class SetTimeSubMenu : SubMenu
{
    [SerializeField] private AlarmClockMenu alarmClockMenu;
    [SerializeField] private TimeSelector timeSelector;
    
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    private int _oldHour;
    private int _oldMinute;
    
    private void Awake()
    {
        saveButton.onClick.AddListener(Save);
        cancelButton.onClick.AddListener(Cancel);
    }

    public override void Activate()
    {
        base.Activate();
        SaveOldTime();
        timeSelector.Reset();
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
        alarmClockMenu.SetTime();
        Deactivate();
    }

    private void Cancel()
    {
        CancelTime();
        Deactivate();
    }
}