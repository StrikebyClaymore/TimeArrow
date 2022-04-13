using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TimeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Coroutine _press;
    private float _pressTime = 0.5f;
    private UnityAction SelectTime;
    private UnityAction SetTime;
    
    public void ConnectActions(UnityAction select, UnityAction set)
    {
        SelectTime = select;
        SetTime = set;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _press = StartCoroutine(LongDown());
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_press is null)
            return;
        StopCoroutine(_press);
        SelectTime();
    }

    private IEnumerator LongDown()
    {
        yield return new WaitForSeconds(_pressTime);
        _press = null;
        SetTime();
    }
}
