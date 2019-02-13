using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncColorblind : MonoBehaviour
{
    public Transform[] cameras;
    public int mode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            mode = 1;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            mode = 2;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            mode = 0;
        }

        foreach(Transform cam in cameras)
        {
            cam.GetComponent<ColorblindScript>().mode = mode;
        }
    }
}
