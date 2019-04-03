﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerJigsaw : MonoBehaviour
{
    public Text openUI;
    private GameObject player;
    public bool inPuzzle;
    public PerspectiveSwap vantageMgr;
    private GameObject puzzle;
    public bool puzzleTriggeredOnce;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        openUI.text = "";
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject puzzle = GameObject.Find("Canvas-Jigsaw(Clone)");
        if(inPuzzle && puzzle == null)
        {
            inPuzzle = false;
            if (door != null)
                door.GetComponent<Animator>().SetTrigger("DoorOpenIn");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (puzzleTriggeredOnce)
            {
                openUI.text = "";
                return;
            }

            if (vantageMgr.playerActive)
            {
                openUI.text = "'E' to Open.";
                if (Input.GetKeyDown(KeyCode.E) && !inPuzzle)
                {

                    player.GetComponent<Level2Script>().subPage();
                    puzzleTriggeredOnce = true;
                    inPuzzle = true;
                    Instantiate(Resources.Load("Canvas-Jigsaw"));
                }
            }
            else
            {
                openUI.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            openUI.text = "";
        }
    }
}
