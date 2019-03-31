﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScriptL3 : MonoBehaviour
{
    public GameObject door;
    public GameObject fan;
    public float increment;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!fan.GetComponent<VisionObjectScript>().enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, increment * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        //Open Door
        if (door != null)
            door.GetComponent<Animator>().SetTrigger("OpenIn");
        //Activate any UI
    }
}