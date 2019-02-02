using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Watch_Vision : MonoBehaviour
{
    public GameObject vision_Objects;
    public Time_Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        vision_Objects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            vision_Objects.SetActive(true);
            DateTime startTime = DateTime.Now;
            bank.Update_time(startTime);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            vision_Objects.SetActive(false);
        }

    }
}
