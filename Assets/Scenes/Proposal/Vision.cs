using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public GameObject mouseUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool activeUI = false;
        //bool decreasePower = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Vision"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                PerspectiveScript pScript = hit.transform.gameObject.GetComponent<PerspectiveScript>();
                if (pScript.enabled && pScript.active && hit.distance > 2.2f)
                {
                    activeUI = true;
                    // print("OOH YEAH");
                    
                    // on down: set current target
                    // on held: add power
                    // on release: reset current target, reset power
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Change colliders to be interactable
                        if (hit.collider.GetType() == typeof(MeshCollider))
                        {
                            MeshCollider mCollider = (MeshCollider)hit.collider;
                            mCollider.isTrigger = false;
                            mCollider.convex = false;
                        }
                        else
                        {
                            hit.collider.isTrigger = false;
                        }

                        // Alter materials as necessary
                        Material realMat = pScript.realMat;
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material = realMat;
                        // Disable the PerspectiveScript component
                        hit.transform.gameObject.GetComponent<PerspectiveScript>().enabled = false;
                        // Activate any components necessary for special effects
                    }
                }
            } else
            {
                //decreasePower = true;
               // print("Oh.");
                // power = 0;
            }
            if(activeUI)
            {
                //hit.transform.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            } else
            {
                //hit.transform.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }
            mouseUI.SetActive(activeUI);
        }
    }
}
