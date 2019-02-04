using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Watch_Vision : MonoBehaviour
{
    public GameObject vision_Objects;
    public Time_Bank bank;
    private Boolean in_Vision;
    DateTime startTime;

    // Start is called before the first frame update
    void Start()
    {
        vision_Objects.SetActive(false);
        in_Vision = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!in_Vision)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {

                startTime = DateTime.Now;
                in_Vision = true;
                vision_Objects.SetActive(true);
                bank.Update_time(startTime);
            }

        }
        else
        {
            bank.Update_time(startTime);
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {

                in_Vision = false;
                vision_Objects.SetActive(false);
                bank.Amount_Of_Time = int.Parse(bank.timerLabel.text);
            }
        }




    }
}
