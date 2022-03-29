using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
