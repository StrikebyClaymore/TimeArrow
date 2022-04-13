using System;
using UnityEngine;
using UnityEngine.UI;

public class DayButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject onImage;

    public void ConnectOnClick(Action<int> onSetDayAction, int idx)
    {
        button.onClick.AddListener(() => onSetDayAction(idx));
    }

    public void ChangeState(int idx)
    {
        if (onImage.activeSelf)
            Off(idx);
        else
            On(idx);
    }
    
    private void On(int idx)
    {
        onImage.SetActive(true);
    }
    
    private void Off(int idx)
    {
        onImage.SetActive(false);
    }
}
