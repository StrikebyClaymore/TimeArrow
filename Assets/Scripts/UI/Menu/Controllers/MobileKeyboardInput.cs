using System;
using UnityEngine;

public class MobileKeyboardInput : MonoBehaviour
{
    [SerializeField] private KeyboardInputView ui;

    private TouchScreenKeyboard _keyboard;
    private string _inputText = "";
    
    private Action<int> SendInput;
    private Action<int> SendHour;
    private Action<int> SendMinute;
    
    private SetTimeSubMenu.SetTimeTypeEnum _selectTimeType;

    private void Awake()
    {
        ConnectActions();
    }

    public void Init(Action<int> sendHour, Action<int> sendMinute)
    {
        SendHour = sendHour;
        SendMinute = sendMinute;
    }
    
    private void Update()
    {
        InputProcess();
    }

    private void InputProcess()
    {
        if (!Input.anyKeyDown)
            return;

        if (Input.inputString == " ")
        {
            CloseKeyBoard();
            return;
        }

        _inputText += Input.inputString;
        
        if (_inputText.Length > 2)
        {
            var text = _inputText;
            text = text.Substring(text.Length - 2, text.Length - 1);
            _inputText = text;
        }
        
        SetTime();
    }

    public void OpenKeyboard(SetTimeSubMenu.SetTimeTypeEnum type)
    {
        enabled = true;
        ui.Show();
        _selectTimeType = type;

        switch (type)
        {
            case SetTimeSubMenu.SetTimeTypeEnum.Minute:
                SendInput = SendMinute;
                _inputText = AlarmClockManager.AlarmClock.Minute.ToString();
                break;
            default:
                _inputText = AlarmClockManager.AlarmClock.Hour.ToString();
                SendInput = SendHour;
                break;
        }
        
        ui.SetTimeText(SetTimeSubMenu.SetTimeTypeEnum.Minute, AlarmClockManager.AlarmClock.Minute);
        ui.SetTimeText(SetTimeSubMenu.SetTimeTypeEnum.Hour, AlarmClockManager.AlarmClock.Hour);
    }

    private void SetTime()
    {
        int.TryParse(_inputText, out var time);
        ui.SetTimeText(_selectTimeType, time);
        SendInput(time);
    }
    
    private void CloseKeyBoard()
    {
        enabled = false;
        ui.Hide();
    }

    private void SetHour()
    {
        SendInput = SendHour;
        _selectTimeType = SetTimeSubMenu.SetTimeTypeEnum.Hour;
        _inputText = AlarmClockManager.AlarmClock.Hour.ToString();

    }

    private void SetMinute()
    {
        SendInput = SendMinute;
        _selectTimeType = SetTimeSubMenu.SetTimeTypeEnum.Minute;
        _inputText = AlarmClockManager.AlarmClock.Minute.ToString();
    }
    
    private void ConnectActions()
    {
        ui.OnSetHour += SetHour;
        ui.OnSetMinute += SetMinute;
    }
    
    private void DisconnectActions()
    {
        ui.OnSetHour -= SetHour;
        ui.OnSetMinute -= SetMinute;
    }
    
    /*private void InputProcess()
    {
        if (_keyboard == null)
            return;

        if (_keyboard.text.Length > 2)
        {
            var text = _keyboard.text;
            text = text.Substring(text.Length - 3, text.Length - 1);
            _keyboard.text = text;
        }

        int.TryParse(_keyboard.text, out var time);
        SetTime(time);
        
        if(_keyboard.status != TouchScreenKeyboard.Status.Done)
            return;
        _inputText = _keyboard.text;
        _keyboard = null;
        
        int.TryParse(_inputText, out var number);
        SendInput(number);
        
        enabled = false;
    }
    
    public void OpenKeyboard(Action<int> action, SetTimeSubMenu.SetTimeTypeEnum type)
    {
        enabled = true;
        SendInput = action;

        var timeSpan = TimeSpan.FromMinutes(AlarmClockManager.AlarmClock.Minute);
        minuteText.text = timeSpan.ToString("mm");
        timeSpan = TimeSpan.FromHours(AlarmClockManager.AlarmClock.Hour);
        hourText.text = timeSpan.ToString("hh");

        _inputText = type == SetTimeSubMenu.SetTimeTypeEnum.Hour ? hourText.text : minuteText.text;
        
        _keyboard = TouchScreenKeyboard.Open(_inputText, TouchScreenKeyboardType.NumberPad, false, false, true);

        _selectedTimeType = type;
        
        if (ScreenOrientationManager.Orientation == DeviceOrientation.Portrait)
            return;
        mainPanel.SetActive(false);
        keyboardInputPanel.SetActive(true);
    }

    public void CloseKeyBoard()
    {
        if (ScreenOrientationManager.Orientation == DeviceOrientation.Portrait)
            return;
        keyboardInputPanel.SetActive(false);
        mainPanel.SetActive(true);
    }*/
}