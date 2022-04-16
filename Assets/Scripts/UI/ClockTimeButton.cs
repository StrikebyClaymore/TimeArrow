using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClockTimeButton : MonoBehaviour
{
    [HideInInspector]
    public int time;
    [HideInInspector]
    public SetTimeSubMenu.SetTimeType type; 
    [HideInInspector]
    public bool selected;
    private bool _picked; 
    
    private UnityEvent OnMouseUpEvent = new UnityEvent();
    private UnityEvent OnMouseOverEvent = new UnityEvent();

    public void Init(int time, SetTimeSubMenu.SetTimeType type)
    {
        this.time = time;
        this.type = type;
    }
    
    public void ConnectActions(Action<ClockTimeButton> onMouseUp, Action<ClockTimeButton> onMouseOver)
    {
        OnMouseUpEvent.AddListener(() => onMouseUp(this));
        OnMouseOverEvent.AddListener(() => onMouseOver(this));
    }

    private void OnMouseDown()
    {
        selected = true;
    }

    private void OnMouseUp()
    {
        if (selected)
            OnMouseUpEvent?.Invoke();
    }

    private void OnMouseEnter()
    {
        selected = true;
    }

    private void OnMouseExit()
    {
        selected = false;
        _picked = false;
    }

    private void OnMouseOver()
    {
        if (_picked)
            return;
        _picked = true;
        OnMouseOverEvent?.Invoke();
    }
}
