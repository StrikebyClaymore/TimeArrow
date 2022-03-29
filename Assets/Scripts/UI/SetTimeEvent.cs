using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SetTimeEvent : UnityEvent<Vector3, int, TimeSelector.SetTimeTypeEnum> { }