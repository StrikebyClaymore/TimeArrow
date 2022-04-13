using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DaysText : MonoBehaviour
{
    [SerializeField] private Transform allDaysOff;
    [SerializeField] private Transform allDays;
    [SerializeField] private GameObject allDaysOn;
    [SerializeField] private GameObject weekdays;
    [SerializeField] private GameObject weekend;

    public void UpdateText()
    {
        DeactivateAll();
        var daysOn = AlarmClockManager.AlarmClock.DaysOn;
        switch (daysOn.Count(day => day == true))
        {
            case 5:
                if (!daysOn[5] && !daysOn[6])
                    weekdays.SetActive(true);
                else
                    SetDaysOn();
                break;
            case 2:
                if(daysOn[5] && daysOn[6])
                    weekend.SetActive(true);
                else
                    SetDaysOn();
                break;
            case 7:
                allDaysOn.SetActive(true);
                break;
            default:
                SetDaysOn();
                break;
        }
    }

    private void SetDaysOn()
    {
        SetChildrenTextEnable(allDaysOff, true);
        var daysOn = AlarmClockManager.AlarmClock.DaysOn;
        for (int i = 0; i < daysOn.Length; i++)
        {
            if (daysOn[i])
                allDays.GetChild(i).GetComponent<Text>().enabled = true;
        }
    }
    
    private void DeactivateAll()
    {
        SetChildrenTextEnable(allDaysOff, false);
        SetChildrenTextEnable(allDays, false);
        allDaysOn.SetActive(false);
        weekdays.SetActive(false);
        weekend.SetActive(false);
    }

    private void SetChildrenTextEnable(Transform parent, bool enable)
    {
        foreach (Transform child in parent)
        {
            child.GetComponent<Text>().enabled = enable;
        }
    }
}
