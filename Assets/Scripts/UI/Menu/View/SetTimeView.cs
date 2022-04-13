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
    [SerializeField] private TimeButton hoursButton;
    [SerializeField] private TimeButton minutesButton;

    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    public Action OnSave;
    public Action OnCancel;
    public Action OnSelectHours;
    public Action OnSetHours;
    public Action OnSelectMinutes;
    public Action OnSetMinutes;
    public Action<SetTimeSubMenu.SetTimeTypeEnum, int> OnSetTime;
    public Action<SetTimeSubMenu.SetTimeTypeEnum, int> OnSelectTime;

    private void Start()
    {
        saveButton.onClick.AddListener(SaveClick);
        cancelButton.onClick.AddListener(CancelClick);
        hoursButton.ConnectActions(SelectHoursClick, SetHoursClick);
        minutesButton.ConnectActions(SelectMinutesClick, SetMinutesClick);
    }

    public void Init()
    {
        builder.Init(this);
        
        timeSelector.rootUI = this;
    }

    public void Reset()
    {
        SetSelectHours();
        lineRenderer.SetPosition(0, hoursClock.transform.position);
        var position = hoursContainer.GetChild(TimeToHourChildIndex(0)).position;
        lineRenderer.SetPosition(1, position);
        circle.position = position;
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
                timeSpan = TimeSpan.FromMinutes(time);
                minuteText.text = timeSpan.ToString("mm");
                position = minutesContainer.GetChild(time).position;
                break;
            default:
                timeSpan = TimeSpan.FromHours(time);
                hourText.text = timeSpan.ToString("hh");
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
    
    private void SetHoursClick() => OnSetHours?.Invoke();
    
    private void SelectMinutesClick() => OnSelectMinutes?.Invoke();
    
    private void SetMinutesClick() => OnSetMinutes?.Invoke();
    
    private void SaveClick() => OnSave?.Invoke();

    private void CancelClick() => OnCancel?.Invoke();
}