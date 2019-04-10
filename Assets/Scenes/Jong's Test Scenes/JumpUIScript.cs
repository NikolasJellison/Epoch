using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpUIScript : MonoBehaviour
{
    public Text moveUI;
    private ClimbableScript[] climbables;
    public GameObject player;
    public GameObject vantageMgr;
    // Start is called before the first frame update
    void Start()
    {
        climbables = Object.FindObjectsOfType<ClimbableScript>();
    }

    // Update is called once per frame
    void Update()
    {
        bool activeUI = false;
        foreach (ClimbableScript climbable in climbables)
        {
            if (climbable.inContact)
            {
                activeUI = true;
                break;
            }
        }

        if (activeUI && vantageMgr.GetComponent<PerspectiveSwap>().playerActive)
        {
            moveUI.text = "'Space' to climb up.";
        }
        else
        {
            moveUI.text = "";
        }
    }
}
