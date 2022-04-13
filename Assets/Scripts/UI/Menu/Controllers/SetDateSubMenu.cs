public class SetDateSubMenu : SubMenuController<SetDateView, AlarmClockMenu>
{
    private bool[] _daysOldState;

    protected override void Awake()
    {
        base.Awake();
        ConnectActions();
    }

    public override void Activate()
    {
        base.Activate();
        SaveOldState();
    }

    private void SaveOldState()
    {
        _daysOldState = (bool[]) AlarmClockManager.AlarmClock.DaysOn.Clone();
    }
    
    private void CancelChanges()
    {
        for (int i = 0; i < _daysOldState.Length; i++)
        {
            if(_daysOldState[i] == AlarmClockManager.AlarmClock.DaysOn[i])
                continue;
            SetDay(i);
        }
    }

    public void SetDay(int idx)
    {
        foreach (var ui in uiArray)
        {
            ui.SetDayClick(idx);
        }
        AlarmClockManager.AlarmClock.SetDay(idx);
    }
    
    private void Save()
    {
        rootController.SetDate();
        rootController.currentSubMenu = null;
        Deactivate();
    }

    private void Cancel()
    {
        CancelChanges();
        rootController.currentSubMenu = null;
        Deactivate();
    }
    
    private void ConnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnSave += Save;
            ui.OnCancel += Cancel;
            ui.OnSetDay += SetDay;
        }
    }
    
    private void DisconnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnSave -= Save;
            ui.OnCancel -= Cancel;
            ui.OnSetDay -= SetDay;
        }
    }
}
