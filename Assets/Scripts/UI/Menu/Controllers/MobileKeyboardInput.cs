using System;
using UnityEngine;

public class MobileKeyboardInput : MonoBehaviour
{
    [SerializeField] private KeyboardInputView ui;

    private TouchScreenKeyboard _keyboard;
    private string _inputText = "";
    
    private Action<int, int> SendInput;

    private SetTimeSubMenu.SetTimeTypeEnum _selectTimeType;

    private void Awake()
    {
        ConnectActions();
    }

    public void Init(Action<int, int> sendTime)
    {
        SendInput = sendTime;
    }
    
    private void Update()
    {
        InputProcess();
    }

    private void InputProcess()
    {
        TestInput();
        
#if UNITY_ANDROID

        if (_keyboard == null)
            return;

        if (_keyboard.text.Length > 4)
        {
            var text = _keyboard.text;
            text = text.Substring(text.Length - 4, text.Length - 1);
            _keyboard.text = text;
        }

        if (_inputText != _keyboard.text)
        {
            _inputText = _keyboard.text;
            SetTime();    
        }

        if (_keyboard.status == TouchScreenKeyboard.Status.Canceled)
        {
            Debug.Log("CANCEL " + _keyboard.active);
            _keyboard.active = true;
        }

        if(_keyboard.status != TouchScreenKeyboard.Status.Done)
            return;
        
        CloseKeyBoard();
        
#endif
    }

    private void TestInput()
    {
#if UNITY_EDITOR

        if (!Input.anyKeyDown)
            return;

        if (Input.inputString == " ")
        {
            CloseKeyBoard();
            return;
        }
        
        _inputText += Input.inputString;

        if (_inputText.Length > 4)
        {
            var text = _inputText;
            text = text.Substring(text.Length - 4, text.Length - 1);
            _inputText = text;
        }
        
        SetTime();
#endif
    }

    public void OpenKeyboard()
    {
        enabled = true;
        ui.Show();

        SetTimeToInputText();
        
        var h = AlarmClockManager.AlarmClock.Hour;
        var m = AlarmClockManager.AlarmClock.Minute;
        ui.SetTimeText(h, m);

#if UNITY_ANDROID
        
        _keyboard = TouchScreenKeyboard.Open(_inputText, TouchScreenKeyboardType.NumberPad, false, false, true);
        
#endif
    }

    private void CloseKeyBoard()
    {
        enabled = false;
        _keyboard = null;
        ui.Hide();
    }

    private void SetTime()
    {
        int.TryParse(_inputText.Substring(0, 2), out var hour);
        int.TryParse(_inputText.Substring(2, 2), out var minute);
        
        hour = Mathf.Min(hour, 24);
        minute = Mathf.Min(minute, 59);
        
        ui.SetTimeText(hour,minute);
        SendInput(hour, minute);
    }
    
    private void OpenSetTime()
    {
        SetTimeToInputText();
        
#if UNITY_ANDROID
        
        _keyboard = TouchScreenKeyboard.Open(_inputText, TouchScreenKeyboardType.NumberPad, false, false, true);
        
#endif
    }

    private void SetTimeToInputText()
    {
        var h = AlarmClockManager.AlarmClock.Hour;
        var m = AlarmClockManager.AlarmClock.Minute;
        
        ui.SetTimeText(h, m);
        
        var timeSpan = TimeSpan.FromHours(h);
        var text = timeSpan.ToString("hh");
        timeSpan = TimeSpan.FromMinutes(m);
        text += timeSpan.ToString("mm");
        
        _inputText = text;
    }

    private void ConnectActions()
    {
        ui.OnOpenSetTime += OpenSetTime;
    }
    
    private void DisconnectActions()
    {
        ui.OnOpenSetTime -= OpenSetTime;
    }
}