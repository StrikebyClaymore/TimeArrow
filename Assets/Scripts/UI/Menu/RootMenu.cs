using System;
using UnityEngine;

public class RootMenu : MonoBehaviour
{
    public ClockMenu clockMenu;
    public AlarmClockMenu alarmClockMenu;
    [HideInInspector]
    public BaseMenuController currentMenu;
    
    public enum MenuTypeEnum
    {
        ClockMenu,
        AlarmClockMenu
    }

    private void Awake()
    {
        clockMenu.root = this;
        alarmClockMenu.root = this;
    }

    private void Start()
    {
        ChangeController(MenuTypeEnum.ClockMenu);
        ChangeOrientation();
    }

    public void ChangeController(MenuTypeEnum menu)
    {
        DeactivateControllers();
        
        switch (menu)
        {
            case MenuTypeEnum.ClockMenu:
                clockMenu.Activate();
                currentMenu = clockMenu;
                break;
            case MenuTypeEnum.AlarmClockMenu:
                alarmClockMenu.Activate();
                currentMenu = alarmClockMenu;
                break;
            default:
                break;
        }
    }

    public void ChangeOrientation()
    {
        currentMenu.ChangeOrientation();
    }

    private void DeactivateControllers()
    {
        clockMenu.Deactivate();
        alarmClockMenu.Deactivate();
    }
}
