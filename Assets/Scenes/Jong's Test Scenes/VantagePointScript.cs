﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VantagePointScript : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        int mask = 1 << 2;
        mask = mask | (1 << 5);
        mask = ~mask;
        //print("doot?");
        //set cursor to default
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(ray.origin, ray.direction*hit.distance, Color.yellow);
            Transform parent = hit.collider.transform.parent;
            
            if (parent != null && parent.CompareTag("Vision"))
            {
                VisionObjectScript vOScript = parent.GetComponent<VisionObjectScript>();
                if (vOScript != null && vOScript.enabled)
                {
                    //print("OH BOY");
                    // Change Cursor

                    if (Input.GetMouseButtonDown(0))
                    {
                        AkSoundEngine.PostEvent("RevealStinger", gameObject);
                        vOScript.enabled = false;
                    }
                }
            }
        }
    }
}