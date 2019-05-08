﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerScript : MonoBehaviour
{
    public Text collectUI;
    public GameObject speaker;
    // Start is called before the first frame update
    void Start()
    {
        collectUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if ((GetComponent<VisionObjectScript>() == null || !GetComponent<VisionObjectScript>().enabled) &&
                !other.gameObject.GetComponent<PlayerController>().lock_movement)
            {
                collectUI.text = "Left Click to pick up";
                if (Input.GetMouseButtonDown(0))
                {
                    collectUI.text = "";
                    if (speaker != null && speaker.GetComponent<AudioSource>() != null)
                    {
                        speaker.GetComponent<AudioSource>().Play();
                    }
                    gameObject.SetActive(false);
                }
            }
            else
            {
                collectUI.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectUI.text = "";
        }
    }
}
