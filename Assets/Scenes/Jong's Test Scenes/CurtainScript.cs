using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurtainScript : MonoBehaviour
{

    public Text moveText;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        moveText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!start)
            {
                moveText.text = "'E' to move aside";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GetComponent<Animator>().SetTrigger("Open");
                    start = true;
                }
            }
            else
            {
                moveText.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moveText.text = "";
        }
    }
    //*/
}
