using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DestroyPromptScript : MonoBehaviour
{
    public Text promptUI;
    public bool hit;
    public PerspectiveSwap vantageMgr;
    public GameObject hammer;

    // Start is called before the first frame update
    void Start()
    {
        promptUI.text = "";
    }
    private void Update()
    {
        // if the hammer is inactive OR the UI is on OR you're in a vantage point turn off the hammer icon
        // otherwise, set the hammer ui
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (hit || !vantageMgr.playerActive)
            {
                promptUI.text =      "";
                return;
            }

            if(hammer != null && !hammer.activeSelf)
            {
                // if the hammer is active, set the text to say that you need something to break it
                // otherwise, E to destroy and accept E as an input
                promptUI.text = "'E' to destroy.";
                //Debug.Log("manny false");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit = true;
                    GetComponent<Destruction>().EnableDestruction();
                }
            } else
            {
                promptUI.text = "You need something to break this.";
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
           
            promptUI.text = "";
        }
    }
}
