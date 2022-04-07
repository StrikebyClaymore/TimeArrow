using UnityEngine;
using UnityEngine.UI;

public class AlarmSubMenu : BaseMenuController
{
    [SerializeField] private Button stopButton;
    [SerializeField] private Button postponeButton;
    
    private void Awake()
    {
        stopButton.onClick.AddListener(StopAlarmButtonClick);
        postponeButton.onClick.AddListener(PostponeAlarmButtonClick);
    }

    private void StopAlarmButtonClick()
    {
        ApplicationManager.AlarmClockManager.StopAlarm();
        Deactivate();
    }
    
    private void PostponeAlarmButtonClick()
    {
        ApplicationManager.AlarmClockManager.StopAlarm();
        ApplicationManager.AlarmClockManager.PostponeAlarm();
        Deactivate();
    }
}
