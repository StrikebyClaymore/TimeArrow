using UnityEngine;

public class ClockBuilder : MonoBehaviour
{
    [SerializeField] private Transform hoursContainer;
    [SerializeField] private Transform minutesContainer;
    [SerializeField] private ClockTimeButton minuteClockButtonPrefab;
    [SerializeField] private RectTransform minutesFaceImage;

    private SetTimeView _rootUI;

    public void Init(SetTimeView rootUI)
    {
        _rootUI = rootUI;
        InitSelectableObjects();
    }

    private void InitSelectableObjects()
    {
        InitHours();
        InitMinutes();
    }

    private void InitHours()
    {
        for (int i = 0; i < hoursContainer.childCount; i++)
        {
            var child = hoursContainer.GetChild(i);
            var clockTimeButton = child.GetComponent<ClockTimeButton>();
            var time = i;
            switch (i)
            {
                case 0:
                    time = 12;
                    break;
                case 23:
                    time = 0;
                    break;
                default:
                    if(i > 11)
                        time = i + 1;
                    break;
            }
            clockTimeButton.Init(time, SetTimeSubMenu.SetTimeType.Hour);
            clockTimeButton.ConnectActions(_rootUI.OnSetTime, _rootUI.OnSelectTime);
        }
    }

    private void InitMinutes()
    {
        minutesContainer.GetChild(0).GetComponent<ClockTimeButton>().Init(0, SetTimeSubMenu.SetTimeType.Minute);
        minutesContainer.GetChild(0).GetComponent<ClockTimeButton>().ConnectActions(_rootUI.OnSetTime, _rootUI.OnSelectTime);
        
        var radius = minutesFaceImage.sizeDelta.x/2;

        for (int i = 1; i < 60; i++)
        {
            var angle = i * -6f;
            var rotation = Quaternion.Euler(0, 0, angle);
            var pos = rotation * Vector3.up * radius;
            var clockTimeButton = Instantiate(minuteClockButtonPrefab, Vector3.zero, rotation, minutesContainer);
            clockTimeButton.transform.localPosition = pos;
            clockTimeButton.Init(i, SetTimeSubMenu.SetTimeType.Minute);
            clockTimeButton.ConnectActions(_rootUI.OnSetTime, _rootUI.OnSelectTime);
        }
    }
}
