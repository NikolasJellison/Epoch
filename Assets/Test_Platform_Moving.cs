using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Platform_Moving : MonoBehaviour
{
    public float speed;
    public GameObject waypoint_1;
    public GameObject waypoint_2;



    void Start()
{
        transform.position = waypoint_1.transform.position;
}

// Update is called once per frame
void Update(){
        if (Input.GetKey(KeyCode.T))
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint_2.transform.position, Time.deltaTime * speed);
        }

    }
}
