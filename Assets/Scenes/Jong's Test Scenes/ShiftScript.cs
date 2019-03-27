using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftScript : MonoBehaviour
{
    public Transform target;
    private bool triggered;
    public float increment;
    public Text textWS2;
    private GameObject player;
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

        if (triggered && Vector3.Distance(transform.position, target.position) > increment)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, increment * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggered)
            {
                textWS2.text = "";
                return;
            }
            textWS2.text = "'E' to Move.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                triggered = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggered)
            {
                textWS2.text = "";
                return;
            }
            textWS2.text = "'E' to Move.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                triggered = true;
            }
        }
    }
}
