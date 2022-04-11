using System;
using UnityEngine;

[RequireComponent(
    typeof(AlarmClockManager),
    typeof(RootMenu))]
public class ApplicationManager : MonoBehaviour
{
    public static AlarmClockManager AlarmClockManager;
    public static RootMenu RootMenu;

    private void Awake()
    {
        AlarmClockManager = GetComponent<AlarmClockManager>();
        RootMenu = GetComponentInChildren<RootMenu>(true);
    }
}
