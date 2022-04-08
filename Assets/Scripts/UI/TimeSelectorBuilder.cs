using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSelectorBuilder : MonoBehaviour
{
    [SerializeField] private TimeSelector timeSelector;
    [SerializeField] private Transform hoursContainer;
    [SerializeField] private Transform minutesContainer;
    [SerializeField] private SelectableObject minuteSelectableObjectPrefab;
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
            var selectableObject = child.GetComponent<SelectableObject>();
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
            selectableObject.Init(time, SetTimeSubMenu.SetTimeTypeEnum.Hour);
            selectableObject.ConnectActions(_rootUI.OnSetTime, _rootUI.OnSelectTime);
        }
    }

    private void InitMinutes()
    {
        minutesContainer.GetChild(0).GetComponent<SelectableObject>().Init(0, SetTimeSubMenu.SetTimeTypeEnum.Minute);
        minutesContainer.GetChild(0).GetComponent<SelectableObject>().ConnectActions(_rootUI.OnSetTime, _rootUI.OnSelectTime);
        
        var radius = minutesFaceImage.sizeDelta.x/2;

        for (int i = 1; i < 60; i++)
        {
            var angle = i * -6f;
            var rotation = Quaternion.Euler(0, 0, angle);
            var pos = rotation * Vector3.up * radius;
            var selectableObject = Instantiate(minuteSelectableObjectPrefab, Vector3.zero, rotation, minutesContainer);
            selectableObject.transform.localPosition = pos;
            selectableObject.Init(i, SetTimeSubMenu.SetTimeTypeEnum.Minute);
            selectableObject.ConnectActions(_rootUI.OnSetTime, _rootUI.OnSelectTime);
        }
    }
}
