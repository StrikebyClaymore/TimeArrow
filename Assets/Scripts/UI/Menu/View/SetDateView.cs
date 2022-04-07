using System;
using UnityEngine;
using UnityEngine.UI;

public class SetDateView : UIView
{
    [SerializeField] private Transform dayButtons;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    public Action<int> OnSetDay;
    public Action OnSave;
    public Action OnCancel;

    private void Start()
    {
        saveButton.onClick.AddListener(SaveClick);
        cancelButton.onClick.AddListener(CancelClick);
        ConnectButtons();
    }

    public void SetDayClick(int idx)
    {
        var button = dayButtons.GetChild(idx).GetComponent<DayButton>();
        button.ChangeState(idx);
    }

    private void ConnectButtons()
    {
        for (int i = 0; i < dayButtons.childCount; i++)
        {
            var button = dayButtons.GetChild(i).GetComponent<DayButton>();
            button.ConnectOnClick(OnSetDay, i);
        }
    }
    
    //private void SetDayClick() => OnSetDay?.Invoke();
    
    private void SaveClick() => OnSave?.Invoke();

    private void CancelClick() => OnCancel?.Invoke();
    
}