using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject onImage;

    public void ConnectOnClick(int idx)
    {
        button.onClick.AddListener(() => On(idx));
    }

    public void On(int idx)
    {
        AlarmClockManager.AlarmClock.SetDay(idx);
        onImage.SetActive(!onImage.activeSelf);
    }
}
