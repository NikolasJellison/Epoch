using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveUIScript : MonoBehaviour
{
    public Text moveUI;
    private MoveableDetectScript[] moveables;
    public GameObject player;
    public GameObject vantageMgr;
    // Start is called before the first frame update
    void Start()
    {
        moveables = Object.FindObjectsOfType<MoveableDetectScript>();
    }

    // Update is called once per frame
    void Update()
    {
        bool activeUI = false;
        foreach (MoveableDetectScript moveable in moveables)
        {
            if (moveable.inContact)
            {
                activeUI = true;
                break;
            }
        }

        if (activeUI && vantageMgr.GetComponent<PerspectiveSwap>().playerActive)
        {
            if (player.GetComponent<PlayerController>().manipulating == false)
            {
                moveUI.text = "'E' to move.";
            }
            else
            {
                moveUI.text = "'E' to let go.";
            }
        }
        else
        {
            moveUI.text = "";
        }
    }
}
