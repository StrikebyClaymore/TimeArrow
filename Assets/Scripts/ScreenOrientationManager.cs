using System;
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

    private void Update() // Если мы переходим в фейс ап, то не меняем ориентацию
    {
        if (Input.deviceOrientation == Orientation || Input.deviceOrientation == DeviceOrientation.FaceUp
                                                   || Input.deviceOrientation == DeviceOrientation.FaceDown)
            return;

        Orientation = Input.deviceOrientation;
        
        Debug.Log("0 " + Orientation);
        
        StartCoroutine(ScreenRotation());
    }

    private IEnumerator ScreenRotation()
    {
        Debug.Log("1 " + Orientation);
        
        yield return new WaitForSeconds(ChangeOrientationTime);
        
        /*if (Orientation == DeviceOrientation.Portrait)
            Screen.SetResolution(Screen.width, Screen.height, true);
        else
            Screen.SetResolution(Screen.height, Screen.width, true);*/
        
        ApplicationManager.RootMenu.ChangeOrientation();
        
        Debug.Log("2 " + Orientation);
    }
    
    public void TestChangeOrientation()
    {
        Orientation = Orientation == DeviceOrientation.Portrait ? DeviceOrientation.LandscapeLeft : DeviceOrientation.Portrait;
        ApplicationManager.RootMenu.ChangeOrientation();
    }
}
