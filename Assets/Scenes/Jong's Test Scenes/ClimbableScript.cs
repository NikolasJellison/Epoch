using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableScript : MonoBehaviour
{
    public bool inContact;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inContact)
        {
            Vector3 tempPosition;
            tempPosition.y = gameObject.GetComponent<MeshRenderer>().bounds.max.y;
            tempPosition.x = gameObject.GetComponent<MeshRenderer>().bounds.ClosestPoint(player.transform.position).x;
            tempPosition.z = gameObject.GetComponent<MeshRenderer>().bounds.ClosestPoint(player.transform.position).z;
            player.transform.position = tempPosition;
            tempPosition = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if ((transform.parent != null && 
                transform.GetComponentInParent<VisionObjectScript>() != null && 
                transform.GetComponentInParent<VisionObjectScript>().enabled) ||
                (other.gameObject.GetComponent<PlayerController>() != null && other.gameObject.GetComponent<PlayerController>().manipulating) ||
                transform.position.y < other.gameObject.transform.position.y)
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
        if (other.gameObject.CompareTag("Player"))
        {
            inContact = false;
        }
    }
}
