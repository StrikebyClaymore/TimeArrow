using UnityEngine;

public class MenuChanger : MonoBehaviour
{
    [SerializeField] private RootMenu rootMenu;
    [SerializeField] private ClockMenu clockMenu;
    [SerializeField] private AlarmClockMenu alarmClockMenu;
    [Header("Portrait")]
    [SerializeField] private ClockMenuView clockMenuViewP;
    [SerializeField] private AlarmClockMenuView alarmClockMenuViewP;
    [Header("Landscape")]
    [SerializeField] private ClockMenuView clockMenuViewL;
    [SerializeField] private AlarmClockMenuView alarmClockMenuViewL;
    
}
