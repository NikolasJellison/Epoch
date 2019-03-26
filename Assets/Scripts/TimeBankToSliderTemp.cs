using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBankToSliderTemp : MonoBehaviour
{
    public Time_Bank timeBank;
    public Watch_Vision watchVision;
    public Slider slider;

    private void Start()
    {
        slider.maxValue = timeBank.Amount_Of_Time;
    }
    private void Update()
    {
        slider.value = watchVision.time_left;
    }
}
