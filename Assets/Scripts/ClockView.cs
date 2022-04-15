using System;
using UnityEngine;
using UnityEngine.UI;

public class ClockView : UIView
{
    [SerializeField] private Transform hourArrow;
    [SerializeField] private Transform minuteArrow;
    [SerializeField] private Transform secondArrow;
    [SerializeField] private Text timeText;

    public void Init(DateTime time)
    {
        timeText.text = time.ToString("HH:mm:ss");
    }

    public void RotateArrows(Quaternion h, Quaternion m, Quaternion s)
    {
        secondArrow.rotation = s;
        minuteArrow.rotation = m;
        hourArrow.rotation = h;
    }

    public void UpdateText(DateTime time)
    {
        timeText.text = time.ToString("HH:mm:ss");
    }
    
}
