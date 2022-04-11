using System;
using UnityEngine;

public class UpdateTimer : MonoBehaviour
{
    private bool _isOn;
    private float _maxTime;
    [HideInInspector]
    public float timeLeft;
    private bool _autoReset;
    private Action _action;

    public void Init(float maxTime, bool autoReset, Action action = null, float timeleft_ = 0)
    {
        _maxTime = maxTime;
        timeLeft = timeleft_ <= 0 ? maxTime : timeleft_;
        _autoReset = autoReset;
        _action = action;
        _isOn = true;
    }

    private void Update()
    {
        if (!_isOn)
            return;
        
        timeLeft -= Time.deltaTime;

        if (timeLeft >= 0)
            return;

        _action?.Invoke();

        if(_autoReset)
            timeLeft = _maxTime;
        else
            Destroy(gameObject);
    }
}