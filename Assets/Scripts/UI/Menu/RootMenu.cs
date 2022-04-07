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

    public static DeviceOrientation Orientation = DeviceOrientation.Portrait;
    public void TestChangeOrientation()
    {
        Orientation = Orientation == DeviceOrientation.Portrait ? DeviceOrientation.LandscapeLeft : DeviceOrientation.Portrait;

        currentMenu.ChangeOrientation();
        
        //menuChanger.ChangeView(orientation, clockMenu, alarmClockMenu);
    }
    
    public void ChangeOrientation(DeviceOrientation _orientation)
    {
        /*switch (orientation)
        {
            case DeviceOrientation.Portrait:
                landscape.SetActive(false);
                portrait.SetActive(true);
                break;
            default:
                portrait.SetActive(false);
                landscape.SetActive(true);
                break;
        }*/
        
        //menuChanger.ChangeView(orientation, clockMenu, alarmClockMenu);
    }

    private void DeactivateControllers()
    {
        clockMenu.Deactivate();
        alarmClockMenu.Deactivate();
    }
}
