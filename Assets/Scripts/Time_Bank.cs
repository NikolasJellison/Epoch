using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Time_Bank : MonoBehaviour
{
    public UnityEngine.UI.Text timerLabel;
    public int Time_Left;
    public int Amount_Of_Time;


    public void Start()
    {
        timerLabel.text = Amount_Of_Time.ToString();
        Time_Left = Amount_Of_Time;
    }

    public void Update_time(DateTime initial_time)
    {
        Time_Left = Amount_Of_Time - (DateTime.Now.Second - initial_time.Second);
        timerLabel.text = Time_Left.ToString();
    }

}
