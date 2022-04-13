using System;
using UnityEngine;
using UnityEngine.UI;

public class AlarmClockButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    private bool _enabled;

    public void ConnectOnClick(Action action)
    {
        button.onClick.AddListener(() => action());
    }

    public void ChangeState()
    {
        if (_enabled)
            Off();
        else
            On();
    }
    
    private void On()
    {
        _enabled = true;
        image.sprite = onSprite;
    }
    
    private void Off()
    {
        _enabled = false;
        image.sprite = offSprite;
    }
}
