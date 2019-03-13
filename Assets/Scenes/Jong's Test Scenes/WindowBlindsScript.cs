using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class WindowBlindsScript : MonoBehaviour
{
    public TextMeshProUGUI textWS;
    public Text textWS2;
    public bool start;
    public Transform cord;
    public Transform[] slats;
    private int slatsLeft;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        textWS.text = "";
        textWS2.text = "";
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(start)
        {
            int slatsLeft = slats.Length;
            float targetYPos = slats[0].position[1];

            for (int i = 0; i < slats.Length; ++i)
            {
                // slats[] should start from topmost to bottommost slat
                if(slats[i].position[1] != targetYPos)
                {
                    Vector3 newPos = slats[i].position;
                    newPos[1] = Mathf.Min(targetYPos, slats[i].position[1] + speed);
                    slats[i].position = newPos;
                }
                else
                {
                    slatsLeft -= 1;
                }

                targetYPos -= 0.01f;
            }
            cord.localScale += new Vector3(0,0.005F,0);
            Vector3 newCordPos = cord.position;
            newCordPos[1] -= 0.01f;
            cord.position = newCordPos;

            Vector3 newStringPos = transform.position;
            newStringPos[1] -= 0.05f;
            transform.position = newStringPos;
            if(slatsLeft <= 0)
            {
                GetComponent<WindowBlindsScript>().enabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //print("FLOOP");
        // print(other.tag);
        if (other.CompareTag("Player"))
        {
            textWS.text = "<sprite=\"EKey01\" index=\"0\"> to open.";
            textWS2.text = "'E' to open.";

            if (Input.GetKeyDown(KeyCode.E))
            {
                start = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            textWS2.text = "";
        }
    }
}
