using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public static DateTime ClockTime;
    
    private TimeService _timeService;
    
    [SerializeField] private Transform hourArrow;
    [SerializeField] private Transform minuteArrow;
    [SerializeField] private Transform secondArrow;
    [SerializeField] private Text timeText;

    private Quaternion _hourAngle;
    private Quaternion _minuteAngle;
    private Quaternion _secondAngle;

    private UpdateTimer _checkGlobalTimeTimer;
    private const float MAXTime = 2f;
    private const string TimeLeftSaveName = "TimeLeft";
    
    private void Awake()
    {
        _timeService = new TimeService();
    }

    private void Start()
    {
        _checkGlobalTimeTimer = gameObject.AddComponent<UpdateTimer>();
        _checkGlobalTimeTimer.Init(MAXTime, true, CheckGlobalTime, LoadTimeLeft());

        ClockTime = _timeService.GetTime();

        timeText.text = ClockTime.ToString("HH:mm:ss");
        Invoke(nameof(OnTimeChanged), 1 - ClockTime.Millisecond * 0.001f);
    }

    private void Update()
    {
        ClockTime = ClockTime.AddSeconds(Time.deltaTime);
        _secondAngle = Quaternion.Euler(0, 0, -1 * (ClockTime.Second + (0.001f * ClockTime.Millisecond)) * 6f);
        _minuteAngle = Quaternion.Euler(0, 0, -1 * (ClockTime.Minute + (ClockTime.Second / 60f)) * 6f);
        _hourAngle = Quaternion.Euler(0, 0, -1 * (ClockTime.Hour * 30f + (ClockTime.Minute * 0.5f)));
    }

    private void FixedUpdate()
    {
        secondArrow.rotation = _secondAngle;
        minuteArrow.rotation = _minuteAngle;
        hourArrow.rotation = _hourAngle;
    }

    private void CheckGlobalTime()
    {
        ClockTime = _timeService.GetTime();
    }
    
    private void OnTimeChanged()
    {
        Invoke(nameof(OnTimeChanged), 1 - DateTime.Now.Millisecond * 0.001f);
        timeText.text = ClockTime.ToString("HH:mm:ss");

        ApplicationManager.AlarmClockManager.TimeUpdate();
    }

    private float LoadTimeLeft()
    {
        return PlayerPrefs.GetFloat(TimeLeftSaveName);
    }
    
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(TimeLeftSaveName, _checkGlobalTimeTimer.timeLeft);
    }
}
