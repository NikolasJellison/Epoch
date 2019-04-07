using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalManagerLvl1 : MonoBehaviour
{
    public GameObject player;
    public GameObject options;
    public ToyBox toybox;
    public bool finished;
    public PerspectiveSwap vantageManager;
    public Text directionText;
    public Text goalText;
    public GameObject pivot;

    // Start is called before the first frame update
    void Start()
    {
        directionText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerRoomOneDetection>().blocksFound < 5)
        {
            goalText.text = "Collect the Blocks";
        } else if (!finished)
        {
            if (!pivot.activeSelf)
            {
                pivot.SetActive(true);
            }
            goalText.text = "Find the Toybox";
        } else
        {
            goalText.text = "Find the Exit";
        }

        if (toybox.cutScenePlaying)
        {
            finished = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            if (!finished)
            {
                directionText.text = "There is something I need to do here...";
            } else
            {
                directionText.text = "";
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (!finished)
            {
                directionText.text = "There is something I need to do here...";
            }
            else
            {
                directionText.text = "";
            }
        }

        if (!vantageManager.playerActive || options.activeSelf)
        {
            directionText.text = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            directionText.text = "";
        }
    }
}
