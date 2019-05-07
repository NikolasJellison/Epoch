﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerJigsaw : MonoBehaviour
{
    public Text openUI;
    public GameObject player;
    public bool inPuzzle;
    public PerspectiveSwap vantageMgr;
    private GameObject puzzle;
    public bool puzzleTriggeredOnce;
    public GameObject door;
    public AudioSource whispering;
    public GameObject objectiveLight;
    public GameObject blocker;
    public GameObject pivot;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        openUI.text = "";
       
    }

    // Update is called once per frame
    void Update()
    {
        GameObject puzzle = GameObject.Find("Canvas-Jigsaw(Clone)");
        if(puzzle != null)
        {
            UI.SetActive(false);
        }
        if(inPuzzle && puzzle == null)
        {
            inPuzzle = false;

            if (door != null)
            {
                player.GetComponent<PlayerController>().journalInput = true;
                player.GetComponent<PlayerController>().lock_movement = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                UI.SetActive(true);
                door.GetComponent<Animator>().SetTrigger("DoorOpenIn");
                door.GetComponent<AudioSource>().Play();
                GetComponent<AudioSource>().Play();
                whispering.Stop();
                vantageMgr.swapEnabled = true;
            }
                
        }
        //*/
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
                openUI.text = "'E' to open";
                if (Input.GetKeyDown(KeyCode.E) && !inPuzzle)
                {
                    vantageMgr.swapEnabled = false;
                    player.GetComponent<PlayerController>().journalInput = false;
                    player.GetComponent<PlayerController>().lock_movement = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    player.GetComponent<Level2Script>().pagesFound += 1;
                    puzzleTriggeredOnce = true;
                    pivot.SetActive(true);
                    objectiveLight.SetActive(false);
                    blocker.SetActive(false);
                    Instantiate(Resources.Load("Canvas-Jigsaw"));
                    inPuzzle = true;
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
