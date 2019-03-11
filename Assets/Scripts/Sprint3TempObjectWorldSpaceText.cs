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
    public bool isChair;

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
        textWS2.gameObject.transform.parent.LookAt(textWS2.gameObject.transform.parent.position - player.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(other.GetComponent<PlayerController>().manipulating == false)
            {
                textWS.text = "<sprite=\"EKey01\" index=\"0\"> to move. (Maybe Spam)";
                textWS2.text = "'E' to move. (Maybe Spam)";
                Debug.Log("manny false");
            }
            else
            {
                textWS.text = "<sprite=\"EKey01\" index=\"0\"> to let go. (Maybe Spam)";
                textWS2.text = "'E' to let go. (Maybe Spam)";
                Debug.Log("manny true");
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
