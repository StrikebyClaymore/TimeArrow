using UnityEngine;

public class RootMenu : MonoBehaviour
{
    [Header("Menu")]
    public ClockMenu clockMenu;
    public AlarmClockMenu alarmClockMenu;
    
    public enum MenuTypeEnum
    {
        ClockMenu,
        AlarmClockMenu
    }

    private void Start()
    {
        clockMenu.root = this;
        alarmClockMenu.root = this;

        ChangeController(MenuTypeEnum.ClockMenu);
    }

    public void ChangeController(MenuTypeEnum menu)
    {
        DeactivateControllers();
        
        switch (menu)
        {
            case MenuTypeEnum.ClockMenu:
                clockMenu.Activate();
                break;
            case MenuTypeEnum.AlarmClockMenu:
                alarmClockMenu.Activate();
                break;
            default:
                break;
        }
    }
    
    private void DeactivateControllers()
    {
        clockMenu.Deactivate();
        alarmClockMenu.Deactivate();
    }
}
