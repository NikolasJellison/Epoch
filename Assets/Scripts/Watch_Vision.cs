using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Watch_Vision : MonoBehaviour
{
    //public GameObject vision_objects;
    public GameObject[] vision_objects;
    public UnityEngine.UI.Text timer_label;
    public float time_left;
    public float original_time;

    // Start is called before the first frame update
    void Start()
    {
        //vision_objects.SetActive(false);
        //Find all the objects you can pull into with vision
        vision_objects = GameObject.FindGameObjectsWithTag("Pull_Able");

        timer_label.text = time_left.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && time_left > 0.0)
        {
            time_left -= Time.deltaTime;
            //vision_objects.SetActive(true);

            //Make the objects move to destination

            //need to change this whenever we figure out how we want to select specific pullable objects
            foreach(GameObject vision_object in vision_objects)
            {
                //using the 2nd version of the script right here 
                vision_object.GetComponent<startToEndPoint2>().moveToDestination = true;
            }

        }
        if (Input.GetKeyUp(KeyCode.Mouse1) || time_left <= 0.0)
        {
            //vision_objects.SetActive(false);   

            //Return the objects to their home
            foreach (GameObject vision_object in vision_objects)
            {
                //using the 2nd version of the script right here 
                vision_object.GetComponent<startToEndPoint2>().moveToDestination = false;
            }
        }
        if(time_left < 0.0)
        {
            time_left = 0.0F;
        }


        timer_label.text = ((int)time_left).ToString();
    }
}
