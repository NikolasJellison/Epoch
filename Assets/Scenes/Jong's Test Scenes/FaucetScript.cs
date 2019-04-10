using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetScript : MonoBehaviour
{
    public WaterScript water;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!water.startClear)
        {
            // UI
            if (Input.GetKey(KeyCode.E))
            {
                water.startClear = true;
                water.waterFlow.SetActive(true);
            }
        }
        else
        {
            // UI
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // UI
    }
}
