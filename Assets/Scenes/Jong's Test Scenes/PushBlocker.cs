using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlocker : MonoBehaviour
{
    public GameObject player;
    Transform movingObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().manipulating)
        {
            movingObject = player.transform.GetChild(player.transform.childCount - 1);
            //print(movingObject.name);
            BoxCollider[] colliders = movingObject.GetComponentsInChildren<BoxCollider>();

            foreach (BoxCollider collider in colliders)
            {
                if (!collider.isTrigger)
                {
                    collider.enabled = false;
                }
                    
            }
        }
        GetComponent<BoxCollider>().enabled = player.GetComponent<PlayerController>().manipulating;
        if (!player.GetComponent<PlayerController>().manipulating)
        { 
            if (movingObject != null)
            {
                BoxCollider[] colliders = movingObject.GetComponentsInChildren<BoxCollider>();

                foreach (BoxCollider collider in colliders)
                {
                    if (!collider.isTrigger)
                    {
                        collider.enabled = true;
                    }
                }

                movingObject = null;
            }
        }
    }
}
