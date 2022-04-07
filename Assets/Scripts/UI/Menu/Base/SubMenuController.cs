using UnityEngine;

public abstract class SubMenuController<T, TU> : BaseMenuController<T> where T : UIView where TU : BaseMenuController
{
    [HideInInspector]
    public TU rootController;
    
    public override void Activate()  
    {
        foreach (var ui in uiArray)
        {
            ui.Show();
        }
    }
    
    public override void Deactivate()
    {
        foreach (var ui in uiArray)
        {
            ui.Hide();
        }
    }
}
