﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationManager : MonoBehaviour
{
    public static DeviceOrientation Orientation;
    private const float ChangeOrientationTime = 0.5f;
    
    private void Awake()
    {
        //Orientation = DeviceOrientation.Portrait;
        
        if (Application.platform != RuntimePlatform.Android)
        {
            enabled = false;
            return;
        }

        Orientation = Input.deviceOrientation;
    }

    private void Update()
    {
        if (Input.deviceOrientation == Orientation || Input.deviceOrientation == DeviceOrientation.FaceUp
                                                   || Input.deviceOrientation == DeviceOrientation.FaceDown)
            return;

        Orientation = Input.deviceOrientation;

        StartCoroutine(ScreenRotation());
    }

    private IEnumerator ScreenRotation()
    {
        yield return new WaitForSeconds(ChangeOrientationTime);

        ApplicationManager.RootMenu.ChangeOrientation();
    }
    
    public void TestChangeOrientation()
    {
        Orientation = Orientation == DeviceOrientation.Portrait ? DeviceOrientation.LandscapeLeft : DeviceOrientation.Portrait;
        ApplicationManager.RootMenu.ChangeOrientation();
    }
}
