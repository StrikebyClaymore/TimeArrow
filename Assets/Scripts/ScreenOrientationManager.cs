using System.Collections;
using UnityEngine;

public class ScreenOrientationManager : MonoBehaviour
{
    public static DeviceOrientation Orientation;
    private const float ChangeOrientationTime = 0.5f;
    
    private void Awake()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            enabled = false;
            return;
        }

        if (Input.deviceOrientation != DeviceOrientation.LandscapeLeft &&
            Input.deviceOrientation != DeviceOrientation.LandscapeRight)
            Orientation = DeviceOrientation.Portrait;
        else
            Orientation = DeviceOrientation.LandscapeLeft;

        StartCoroutine(ScreenRotation());
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
