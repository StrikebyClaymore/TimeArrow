using System;
using UnityEngine;

public class MobileKeyboardInput : MonoBehaviour
{
    [SerializeField] private KeyboardInputView ui;

    private TouchScreenKeyboard _keyboard;
    private string _inputText = "";
    private int _hour;
    private int _minute;
    
    private Action<int, int> SendInput;

    private SetTimeSubMenu.SetTimeType _selectTimeType;
    
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

    public void OpenKeyboard(int hour, int minute)
    {
        enabled = true;
        ui.Show();

        SetTimeToInputText();

        _hour = hour;
        _minute = minute;
        ui.SetTimeText(hour, minute);

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
        
        _hour = Mathf.Min(hour, 24);
        _minute = minute > 59 ? 0 : minute;
        
        ui.SetTimeText(_hour, _minute);
        SendInput(_hour, _minute);
    }

    private void SetTimeToInputText()
    {
        ui.SetTimeText(_hour, _minute);
        
        var timeSpan = TimeSpan.FromHours(_hour);
        var text = timeSpan.ToString("hh");
        timeSpan = TimeSpan.FromMinutes(_minute);
        text += timeSpan.ToString("mm");
        
        _inputText = text;
    }
}