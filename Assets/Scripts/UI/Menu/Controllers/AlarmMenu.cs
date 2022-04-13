public class AlarmMenu : BaseMenuController<AlarmMenuView>
{
    protected override void Awake()
    {
        base.Awake();
        ConnectActions();
    }

    private void StopAlarm()
    {
        ApplicationManager.AlarmClockManager.StopAlarm();
        ApplicationManager.RootMenu.ReturnToPrevious();
    }
    
    private void PostponeAlarm()
    {
        ApplicationManager.AlarmClockManager.StopAlarm();
        ApplicationManager.AlarmClockManager.PostponeAlarm();
        ApplicationManager.RootMenu.ReturnToPrevious();
    }
    
    private void ConnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnStopAlarm += StopAlarm;
            ui.OnPostponeAlarm += PostponeAlarm;
        }
    }
    
    private void DisconnectActions()
    {
        foreach (var ui in uiArray)
        {
            ui.OnStopAlarm -= StopAlarm;
            ui.OnPostponeAlarm -= PostponeAlarm;
        }
    }
}
