using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour
{
    [HideInInspector] public TimeSelector timeSelector;
    [HideInInspector] public SetTimeEvent SelectedTime;
    [HideInInspector] public int time;
    [HideInInspector] public TimeSelector.SetTimeTypeEnum type; 
    private bool _selected;

    public void Init(TimeSelector timeSelector, SetTimeEvent ev, int time, TimeSelector.SetTimeTypeEnum type)
    {
        this.timeSelector = timeSelector;
        SelectedTime = ev;
        this.time = time;
        this.type = type;
    }
    
    private void OnMouseUp()
    {
        timeSelector.SetTime(type);
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
            SelectedTime.Invoke(transform.position, time, type);
        }
    }
}
