using System;
using UnityEngine;
using UnityEngine.UI;

public class SetTimeView : UIView
{
    [SerializeField] private TimeSelector timeSelector;
    [SerializeField] private TimeSelectorBuilder builder;
    
    [SerializeField] private GameObject hoursClock;
    [SerializeField] private GameObject minutesClock;
    [SerializeField] private Transform hoursContainer;
    [SerializeField] private Transform minutesContainer;
    
    [SerializeField] private Transform circle;
    [SerializeField] private LineRenderer lineRenderer;
    
    [SerializeField] private Text hourText;
    [SerializeField] private Text minuteText;
    [SerializeField] private Button hoursButton;
    [SerializeField] private Button minutesButton;
    
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    public Action OnSave;
    public Action OnCancel;
    public Action OnSelectHours;
    public Action OnSelectMinutes;
    public Action<SetTimeSubMenu.SetTimeTypeEnum, int> OnSetTime;
    public Action<SetTimeSubMenu.SetTimeTypeEnum, int> OnSelectTime;
    
    public void Init()
    {
        saveButton.onClick.AddListener(SaveClick);
        cancelButton.onClick.AddListener(CancelClick);
        hoursButton.onClick.AddListener(SelectHoursClick);
        minutesButton.onClick.AddListener(SelectMinutesClick);
        
        builder.Init(this);
        
        timeSelector.rootUI = this;
        
        //TimeUpdate(SetTimeSubMenu.SetTimeTypeEnum.Init, 12);
        //SelectTimeUpdate();
    }

    public void TimeUpdate(SetTimeSubMenu.SetTimeTypeEnum type, int time)
    {
        switch (type)
        {
            case SetTimeSubMenu.SetTimeTypeEnum.Hour:
                hoursClock.SetActive(false);
                minutesClock.SetActive(true);
                SelectTimeUpdate(SetTimeSubMenu.SetTimeTypeEnum.Minute, time);
                break;
            case SetTimeSubMenu.SetTimeTypeEnum.Minute:
                break;
            default:
                break;
        }
    }

    public void SelectTimeUpdate(SetTimeSubMenu.SetTimeTypeEnum type, int time)
    {
        lineRenderer.SetPosition(0, hoursClock.transform.position);

        TimeSpan timeSpan;
        Vector3 position;
        
        switch (type)
        {
            case SetTimeSubMenu.SetTimeTypeEnum.Minute:
                //_minute = time;
                timeSpan = TimeSpan.FromMinutes(time);
                minuteText.text = timeSpan.ToString("mm");
                //AlarmClockManager.AlarmClock.Minute = _minute;
                position = minutesContainer.GetChild(time).position;
                break;
            default:
                //_hour = time;
                timeSpan = TimeSpan.FromHours(time);
                hourText.text = timeSpan.ToString("hh");
                //AlarmClockManager.AlarmClock.Hour = _hour;
                position = hoursContainer.GetChild(TimeToHourChildIndex(time)).position;
                break;
        }
        lineRenderer.SetPosition(1, position);
        circle.position = position;
    }

    private int TimeToHourChildIndex(int time)
    {
        var idx = time;
        if (idx == 12)
            return 0;
        if (idx == 0)
            idx = 24;
        if (idx > 11)
            return idx - 1;
        return idx;
    }
    
    public void SetSelectHours()
    {
        minutesClock.SetActive(false);
        hoursClock.SetActive(true);
    }
    
    public void SetSelectMinutes()
    {
        minutesClock.SetActive(true);
        hoursClock.SetActive(false);
    }

    private void SelectHoursClick() => OnSelectHours?.Invoke();
    
    private void SelectMinutesClick() => OnSelectMinutes?.Invoke();
    
    private void SaveClick() => OnSave?.Invoke();

    private void CancelClick() => OnCancel?.Invoke();
}