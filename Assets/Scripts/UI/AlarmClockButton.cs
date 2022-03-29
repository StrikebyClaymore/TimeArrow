using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AlarmClockButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    private bool _enabled;

    private void Start()
    {
        ConnectActions();
    }

    private void ConnectActions()
    {
        button.onClick.AddListener(On);
        button.onClick.AddListener(ApplicationManager.AlarmClockManager.SetAlarmClockActive);
    }

    public void On()
    {
        _enabled = !_enabled;
        image.sprite = _enabled ? onSprite : offSprite;
    }
}
