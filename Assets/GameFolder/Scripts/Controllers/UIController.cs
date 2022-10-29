using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoSingleton<UIController>
{
    [SerializeField] Joystick _joystick;
    [SerializeField] private GameObject winPanel;
    
    public Joystick Joystick => _joystick;

    EventData _eventData;

    void Start()
    {
        Singleton(true);   
        _eventData = Resources.Load("EventData") as EventData;
    }

    private void OnEnable()
    {
        _eventData.OnWin += Win;
    }

    
    private void OnDisable()
    {
        _eventData.OnWin -= Win;
    }

    void Update()
    {
        
    }

    public void Win()
    {
        winPanel.SetActive(true);
    }

    public float Vertical()
    {
        return Joystick.Vertical;
    }

    public float Horizontal()
    {
        return Joystick.Horizontal;
    }
}
