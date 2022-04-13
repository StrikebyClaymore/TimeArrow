using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardInputView : UIView
{
    [SerializeField] private Text hourText;
    [SerializeField] private Text minuteText;
    [SerializeField] private Button timeButton;

    public Action OnOpenSetTime;

    private void Start()
    {
        //timeButton.onClick.AddListener(SelectHourClick);
    }

    public void SetTimeText(int hour, int minute)
    {
        var timeSpan = TimeSpan.FromMinutes(minute);
        minuteText.text = timeSpan.ToString("mm");
        timeSpan = TimeSpan.FromHours(hour);
        hourText.text = timeSpan.ToString("hh");
    }

    private void SelectHourClick() => OnOpenSetTime?.Invoke();
}
