using System;
using UnityEngine;
using UnityEngine.UI;

public class AlarmClockMenuView : UIView
{
    [Header("Buttons")]
    [SerializeField] private Button openSetTimeButton;
    [SerializeField] private Button openSetDateButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    [Header("Text")]
    [SerializeField] private Text timeText;
    [SerializeField] private DaysText dateText;
    
    [Header("Actions")]
    public Action OnOpenSetTime;
    public Action OnOpenSetDate;
    public Action OnSave;
    public Action OnCancel;

    private void Start()
    {
        openSetTimeButton.onClick.AddListener(OpenSetTimeClick);
        openSetDateButton.onClick.AddListener(OpenSetDateClick);
        saveButton.onClick.AddListener(SaveClick);
        cancelButton.onClick.AddListener(CancelClick);
    }

    public void SetTime(string time)
    {
        timeText.text = time;
    }

    public void UpdateDaysText()
    {
        dateText.UpdateText();
    }
    
    private void OpenSetTimeClick() => OnOpenSetTime?.Invoke();

    private void OpenSetDateClick() => OnOpenSetDate?.Invoke();
    
    private void SaveClick() => OnSave?.Invoke();

    private void CancelClick() => OnCancel?.Invoke();
}
