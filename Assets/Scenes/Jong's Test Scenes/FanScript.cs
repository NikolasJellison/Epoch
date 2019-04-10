using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanScript : MonoBehaviour
{
    public NoteScriptL3 note;
    public Text activateUI;
    // Start is called before the first frame update
    void Start()
    {
        activateUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!note.enabled && !GetComponent<VisionObjectScript>().enabled)
            {
                activateUI.text = "'E' to activate the fan";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    note.enabled = true;
                }
            } else
            {
                activateUI.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            activateUI.text = "";
        }
    }
}
