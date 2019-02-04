using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Watch_Vision : MonoBehaviour
{
    public GameObject vision_objects;
    public UnityEngine.UI.Text timer_label;
    public float time_left;

    // Start is called before the first frame update
    void Start()
    {
        vision_objects.SetActive(false);
        timer_label.text = time_left.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && time_left > 0.0)
        {
            time_left -= Time.deltaTime;
            vision_objects.SetActive(true);

        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || time_left <= 0.0)
        {
            vision_objects.SetActive(false);   
        }
        if(time_left < 0.0)
        {
            time_left = 0.0F;
        }


        timer_label.text = ((int)time_left).ToString();
    }
}
