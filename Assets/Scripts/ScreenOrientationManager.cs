using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationManager : MonoBehaviour
{
    private DeviceOrientation _orientation;
    
    /*private void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            enabled = false;
            return;
        }

        _orientation = Input.deviceOrientation;
        ApplicationManager.RootMenu.ChangeOrientation(_orientation);
    }

    private void Update()
    {
        if (Input.deviceOrientation == _orientation)
            return;
        _orientation = Input.deviceOrientation;
        StartCoroutine(ScreenRotation());
    }

    private IEnumerator ScreenRotation()
    {
        yield return new WaitForSeconds(0.5f);
        ApplicationManager.RootMenu.ChangeOrientation(_orientation);
    }*/
}
