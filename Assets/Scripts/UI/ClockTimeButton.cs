using System;
using UnityEngine;
using UnityEngine.Events;

public class ClockTimeButton : MonoBehaviour
{
    [HideInInspector]
    public int time;
    [HideInInspector]
    public SetTimeSubMenu.SetTimeType type; 
    private bool _selected;

    private UnityEvent OnMouseUpEvent = new UnityEvent();
    private UnityEvent OnMouseOverEvent = new UnityEvent();
    
    public void Init(int time, SetTimeSubMenu.SetTimeType type)
    {
        this.time = time;
        this.type = type;
    }
    
    public void ConnectActions(Action<SetTimeSubMenu.SetTimeType, int> onMouseUp, Action<SetTimeSubMenu.SetTimeType, int> onMouseOver)
    {
        OnMouseUpEvent.AddListener(() => onMouseUp(type, time));
        OnMouseOverEvent.AddListener(() => onMouseOver(type, time));
    }

    private void OnMouseUp()
    {
        OnMouseUpEvent?.Invoke();
    }
    
    private void OnMouseExit()
    {
        _selected = false;
    }

    private void OnMouseOver()
    {
        if (_selected)
            return;
        _selected = true;
        OnMouseOverEvent?.Invoke();
    }
}
