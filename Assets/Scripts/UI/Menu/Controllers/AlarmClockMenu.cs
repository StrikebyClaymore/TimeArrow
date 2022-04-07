﻿using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class AlarmClockMenu : BaseMenuController<AlarmClockMenuView>
{
    [Header("SubMenu")]
    [SerializeField] private SetTimeSubMenu setTimeSubMenu;
    [SerializeField] private SetDateSubMenu setDateSubMenu;
    [HideInInspector]
    public BaseMenuController currentSubMenu = null;

    protected override void Awake()
    {
        base.Awake();
        ConnectActions();
    }

    private void Start()
    {
        setTimeSubMenu.root = root;
        setTimeSubMenu.rootController = this;
        setDateSubMenu.root = root;
        setDateSubMenu.rootController = this;
    }

    public override void ChangeOrientation()
    {
        base.ChangeOrientation();
        currentSubMenu.Deactivate();
        currentSubMenu.Activate();
    }

    public void SetTime()
    {
        foreach (var ui in uiArray)
        {
            ui.SetTime(new TimeSpan().FormatToAlarmTime());
        }
    }

    public void SetDate()
    {
        foreach (var ui in uiArray)
        {
            ui.UpdateDaysText();
        }
    }
    
    private void OpenSetTime()
    {
        setTimeSubMenu.Activate();
        currentSubMenu = setTimeSubMenu;
    }

    private void OpenSetDate()
    {
        setDateSubMenu.Activate();
        currentSubMenu = setDateSubMenu;
    }
    
    private void Save()
    {
        root.clockMenu.SetAlarmClock();
        root.ChangeController(RootMenu.MenuTypeEnum.ClockMenu);
    }

    private void Cancel()
    {
        root.ChangeController(RootMenu.MenuTypeEnum.ClockMenu);
    }
    
    private void ConnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnSave += Save;
            ui.OnCancel += Cancel;
            ui.OnOpenSetTime += OpenSetTime;
            ui.OnOpenSetDate += OpenSetDate;
        }
    }
    
    private void DisconnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnSave -= Save;
            ui.OnCancel -= Cancel;
            ui.OnOpenSetTime -= OpenSetTime;
            ui.OnOpenSetDate -= OpenSetDate;
        }
    }
}
