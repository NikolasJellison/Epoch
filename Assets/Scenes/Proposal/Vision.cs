using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public GameObject mouseUI;
    public GameObject[] crosshairs;
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
        int mask = 1 << 2;
        mask = ~mask;
        foreach(GameObject crosshair in crosshairs)
        {
            crosshair.SetActive(false);
        }
        crosshairs[crosshairs.Length - 1].SetActive(true);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.transform.CompareTag("Vision"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                PerspectiveScript pScript = hit.transform.GetComponentInParent<PerspectiveScript>();
                if (pScript != null && pScript.enabled)
                {
                    crosshairs[crosshairs.Length - 1].SetActive(false);
                    int index =  Mathf.RoundToInt(pScript.diff / 9.0f);
                    if (index < 0) index = 0;
                    else if (index > 19) index = 19;
                    print(index);
                    crosshairs[index].SetActive(true);

                    if (pScript.active)
                    {

                        activeUI = true;

                        // on down: set current target
                        // on held: add power
                        // on release: reset current target, reset power
                        if (Input.GetMouseButtonDown(1))
                        {
                            // check for the same target
                            // Change colliders to be interactable
                            /*
                            // Alter materials as necessary
                            Material realMat = pScript.realMat;
                            hit.transform.gameObject.GetComponent<MeshRenderer>().material = realMat;
                            // Disable the PerspectiveScript component
                            hit.transform.gameObject.GetComponent<PerspectiveScript>().enabled = false;
                            // Activate any components necessary for special effects
                            //*/
                            AkSoundEngine.PostEvent("RevealStinger", gameObject);
                            pScript.enabled = false;
                        }
                        /*
                        else if (Input.GetMouseButton(1))
                        {
                            // check for the same target
                            if(power >= 100) {

                            }
                        } 
                        else
                        {   
                            // Check for the same target
                            power = -= 2;
                        }
                        //*/
                    }
                }
            }
            else
            {
                // power = 0;
            }

            if(activeUI)
            {
                //hit.transform.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
            else
            {
                //hit.transform.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }
        }
        mouseUI.SetActive(activeUI);
    }
}
