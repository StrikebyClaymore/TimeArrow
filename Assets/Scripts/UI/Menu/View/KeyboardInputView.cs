using System;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInputView : UIView
{
    [SerializeField] private Text hourText;
    [SerializeField] private Text minuteText;
    [SerializeField] private Button hoursButton;
    [SerializeField] private Button minutesButton;

    public Action OnSetHour;
    public Action OnSetMinute;
    
    private void Start()
    {
        hoursButton.onClick.AddListener(SelectHourClick);
        minutesButton.onClick.AddListener(SelectMinuteClick);
    }
    
    public void SetTimeText(SetTimeSubMenu.SetTimeTypeEnum type, int time)
    {
        TimeSpan timeSpan;
        switch (type)
        {
            case SetTimeSubMenu.SetTimeTypeEnum.Minute:
                time = Mathf.Min(time, 59);
                timeSpan = TimeSpan.FromMinutes(time);
                minuteText.text = timeSpan.ToString("mm");
                break;
            default:
                time = Mathf.Min(time, 24);
                timeSpan = TimeSpan.FromHours(time);
                hourText.text = timeSpan.ToString("hh");
                break;
        }
    }

    private void SelectHourClick() => OnSetHour?.Invoke();

    private void SelectMinuteClick() => OnSetMinute?.Invoke();
}
