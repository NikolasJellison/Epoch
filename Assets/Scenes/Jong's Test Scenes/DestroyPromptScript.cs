using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DestroyPromptScript : MonoBehaviour
{
    public Text textWS2;
    private GameObject player;
    public GameObject sentinel;

    // Start is called before the first frame update
    void Start()
    {
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
        if (other.tag == "Player")
        {
            if (sentinel == null)
            {
                textWS2.text =      "";
                return;
            }
            textWS2.text = "'E' to destroy.";
            //Debug.Log("manny false");
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Destruction>().EnableDestruction();
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
