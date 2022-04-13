using UnityEngine;

public class RootMenu : MonoBehaviour
{
    public ClockMenu clockMenu;
    public AlarmClockMenu alarmClockMenu;
    public AlarmMenu alarmMenu;
    [HideInInspector]
    public BaseMenuController currentMenu;

    public enum MenuTypeEnum
    {
        ClockMenu,
        AlarmClockMenu,
        AlarmMenu,
    }

    private MenuTypeEnum _current;
    private MenuTypeEnum _previous;
    
    private void Awake()
    {
        clockMenu.root = this;
        alarmClockMenu.root = this;
    }

    private void Start()
    {
        _current = MenuTypeEnum.ClockMenu;
        ChangeController(_current);
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
            case MenuTypeEnum.AlarmMenu:
                alarmMenu.Activate();
                currentMenu = alarmMenu;
                break;
            default:
                break;
        }
        
        _previous = _current;
        _current = menu;
    }

    public void ReturnToPrevious()
    {
        ChangeController(_previous);
    }
    
    public void ChangeOrientation()
    {
        currentMenu.ChangeOrientation();
    }

    private void DeactivateControllers()
    {
        clockMenu.Deactivate();
        alarmClockMenu.Deactivate();
        alarmMenu.Deactivate();
    }
}
