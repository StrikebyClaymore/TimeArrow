using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenuController : MonoBehaviour
{
    [HideInInspector]
    public RootMenu root;

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void ChangeOrientation()
    {
        Deactivate();
        Activate();
    }
}

public abstract class BaseMenuController<T> : BaseMenuController where T : UIView
{
    [SerializeField] protected T portraitUI;
    [SerializeField] protected T landscapeUI;
    
    [HideInInspector]
    [SerializeField] protected T[] uiArray;

    protected virtual void Awake()
    {
        AddUI();
    }

    public override void Activate()  
    {
        base.Activate();
        if (ScreenOrientationManager.Orientation == DeviceOrientation.Portrait)
            portraitUI.Show();
        else
            landscapeUI.Show();
    }
    
    public override void Deactivate()
    {
        base.Deactivate();
        foreach (var ui in uiArray)
        {
            ui.Hide();
        }
    }

    private void AddUI()
    {
        uiArray = new T[2] { portraitUI, landscapeUI };
        /*var uiList = new List<T>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).TryGetComponent<T>(out var ui);
            if(ui != null)
                uiList.Add(ui);
        }
        
        uiList.CopyTo(uiArray);*/
    }
}
