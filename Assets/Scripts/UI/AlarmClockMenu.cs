using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmClockMenu : BaseMenu
{
    [Header("SubMenu")]
    [SerializeField] private SetTimeSubMenu setTimeSubMenu;
    [SerializeField] private SetDateSubMenu setDateSubMenu;
    
    [Header("Buttons")]
    [SerializeField] private Button openSetTimeButton;
    [SerializeField] private Button openSetDateButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    [Header("Text")]
    [SerializeField] private Text timeText;
    [SerializeField] private DaysText dateText;

    private void Awake()
    {
        openSetTimeButton.onClick.AddListener(OpenSetTime);
        openSetDateButton.onClick.AddListener(OpenSetDate);
        saveButton.onClick.AddListener(Save);
        cancelButton.onClick.AddListener(Cancel);
    }

    public void SetTime()
    {
        timeText.text = ApplicationManager.AlarmClockManager.FormatTime();
    }

    public void SetDate()
    {
        dateText.UpdateText();
    }
    
    private void Save()
    {
        ApplicationManager.RootMenu.clockMenu.SetAlarmClock();
        root.ChangeController(RootMenu.MenuTypeEnum.ClockMenu);
    }

    private void Cancel()
    {
        root.ChangeController(RootMenu.MenuTypeEnum.ClockMenu);
    }
    
    private void OpenSetTime()
    {
        setTimeSubMenu.Activate();
    }

    private void OpenSetDate()
    {
        setDateSubMenu.Activate();
    }
}
