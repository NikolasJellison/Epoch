using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sprint3TempObjectWorldSpaceText : MonoBehaviour
{
    public TextMeshProUGUI textWS;
    public Text textWS2;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        textWS.text = "";
        textWS2.text = "";
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        //textWS.gameObject.transform.LookAt(transform.position - player.transform.position);
        Vector3 direction = (textWS2.gameObject.transform.parent.position - player.transform.position);
       
        direction[1] = 0.0f;
        textWS2.gameObject.transform.parent.forward = direction;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            // if this is an unactivated vision object, don't show this message
            if(GetComponent<VisionObjectScript>() != null && GetComponent<VisionObjectScript>().enabled)
            {
                return;
            }
            if(other.GetComponent<PlayerController>().manipulating == false)
            {
                textWS.text = "<sprite=\"EKey01\" index=\"0\"> to move.";
                textWS2.text = "'E' to move.";
                //Debug.Log("manny false");
            }
            else
            {
                textWS.text = "<sprite=\"EKey01\" index=\"0\"> to let go.";
                textWS2.text = "'E' to let go.";
                //Debug.Log("manny true");
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            textWS2.text = "";
        }
    }
}
