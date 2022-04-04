using UnityEngine;

public class RootMenu : MonoBehaviour
{
    [Header("Orientation")]
    [SerializeField] private GameObject portrait;
    [SerializeField] private GameObject landscape;
    
    [Header("Menu")]
    public ClockMenu clockMenu;
    public AlarmClockMenu alarmClockMenu;

    [SerializeField] private MenuChanger menuChanger;
       
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

    public void ChangeOrientation(DeviceOrientation orientation)
    {
        switch (orientation)
        {
            case DeviceOrientation.Portrait:
                landscape.SetActive(false);
                portrait.SetActive(true);
                break;
            default:
                portrait.SetActive(false);
                landscape.SetActive(true);
                break;
        }
        
        menuChanger.Change(orientation, clockMenu, alarmClockMenu);
    }

    private void DeactivateControllers()
    {
        clockMenu.Deactivate();
        alarmClockMenu.Deactivate();
    }
}
