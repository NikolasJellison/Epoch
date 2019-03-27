using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerJigsaw : MonoBehaviour
{
    public Text textWS2;
    private GameObject player;
    public bool inPuzzle;
    // Start is called before the first frame update
    void Start()
    {
        textWS2.text = "";
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (textWS2.gameObject.transform.parent.position - player.transform.position);

        direction[1] = 0.0f;
        textWS2.gameObject.transform.parent.forward = direction;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textWS2.text = "'E' to Open.";
            if (Input.GetKeyDown(KeyCode.E) && !inPuzzle)
            {
                inPuzzle = true;
                Instantiate(Resources.Load("Canvas-Jigsaw"));
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
