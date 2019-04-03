using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableDetectScript : MonoBehaviour
{
    public bool inContact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GetComponent<VisionObjectScript>() != null && GetComponent<VisionObjectScript>().enabled)
            {
                inContact = false;
            }
            else
            {
                inContact = true;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GetComponent<VisionObjectScript>() != null && GetComponent<VisionObjectScript>().enabled)
            {
                inContact = false;
            }
            else
            {
                inContact = true;
            }
        }
    }
    //*/

    private void OnTriggerExit(Collider other)
    {
        inContact = false;
    }
}
