using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour
{
    [HideInInspector] public int time;
    [HideInInspector] public SetTimeSubMenu.SetTimeTypeEnum type; 
    private bool _selected;

    private UnityEvent OnMouseUpEvent = new UnityEvent();
    private UnityEvent OnMouseOverEvent = new UnityEvent();
    
    public void Init(int time, SetTimeSubMenu.SetTimeTypeEnum type)
    {
        this.time = time;
        this.type = type;
    }
    
    public void ConnectActions(Action<SetTimeSubMenu.SetTimeTypeEnum, int> onMouseUp, Action<SetTimeSubMenu.SetTimeTypeEnum, int> onMouseOver)
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
        if (!_selected)
        {
            _selected = true;
            OnMouseOverEvent?.Invoke();
        }
    }
}
