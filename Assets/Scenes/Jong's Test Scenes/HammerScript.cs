using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerScript : MonoBehaviour
{
    public Text collectUI;
    // Start is called before the first frame update
    void Start()
    {
        collectUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!GetComponent<VisionObjectScript>().enabled)
            {
                GetComponent<AudioSource>().Play();
                collectUI.text = "Left Click to pick up";
                if (Input.GetMouseButtonDown(0))
                {
                    gameObject.SetActive(false);
                    collectUI.text = "";
                }
            }
            else
            {
                collectUI.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectUI.text = "";
        }
    }
}
