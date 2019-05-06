﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyBox : MonoBehaviour
{
    //This is becoming the gameManager for room 1
    public GameObject cutSceneCamera;
    public GameObject playerCamera;
    public PlayerController controller;
    public Text notificationText;
    public float speed = 2;
    private float step;
    private string notificationMessage;
    [Header("Sentence Order (ITS NOT YOUR FAULT")]
    public GameObject[] sentenceBlocks;
    public Transform[] sentenceBlockLocations;
    public bool cutScenePlaying;
    public GameObject DOOR;
    public GameObject floor;
    private bool stopCallingMyCoroutine;
    public GameObject[] materialObjects;
    public GameObject player;
    //All extra stuff for cutscene
    [Header("Extra Cutscene Stuff")]
    public GameObject BlockMiniGameCanvas;
    private bool raiseBlocks;
    private int correctLetters;
    //MinigameBlocks
    [Header("Minigame Blocks (all 3)")]
    public BlockCutscene[] MGBlocksScript;
    public ParticleSystem MGFinish;
    public GameObject playerUI;
    public PerspectiveSwap perspectiveSwap;
    public GameObject[] cutsceneBlocks;
    public AudioSource victorySound;
    public AudioSource argument;
    public bool decreaseAudio;
    public bool ended;

    private void Start()
    {
        foreach(GameObject block in cutsceneBlocks)
        {
            block.SetActive(false);
        }
        BlockMiniGameCanvas.SetActive(false);
    }

    private void Update()
    {
        if (decreaseAudio && argument.volume > 0.0f)
        {
            print(argument.volume);
            float volume = argument.volume;
            volume -= 0.15f * Time.deltaTime;
            argument.volume = Mathf.Max(0.0f, volume);
        }
        //
        if(player.GetComponent<PlayerRoomOneDetection>().blocksFound == 5)
        {
            foreach(GameObject materialObject in materialObjects)
            {
                if (materialObject.GetComponent<MeshRenderer>().material.GetFloat("_ToyBoxGlow") == 0)
                {
                    materialObject.GetComponent<MeshRenderer>().material.SetFloat("_ToyBoxGlow", 1);
                }
            }
        }

        if (ended)
        {
            foreach (GameObject materialObject in materialObjects)
            {
                if (materialObject.GetComponent<MeshRenderer>().material.GetFloat("_ToyBoxGlow") == 1)
                {
                    materialObject.GetComponent<MeshRenderer>().material.SetFloat("_ToyBoxGlow", 0);
                }
            }
        }
        
        //Debug cutscene
        
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            cutScene();
        }
        
        
        step = speed * Time.deltaTime;
        if (cutScenePlaying)
        {
            cutSceneCamera.transform.LookAt(sentenceBlocks[7].transform.position);
            for (int i = 0; i < sentenceBlocks.Length; i++)
            {
                sentenceBlocks[i].transform.position = Vector3.MoveTowards(sentenceBlocks[i].transform.position, sentenceBlockLocations[i].position, step);
            }

            int counter = 0;
            foreach(BlockCutscene b in MGBlocksScript)
            {
                if (b.inGoal)
                {
                    counter++;
                }
            }
            if (counter == 3)
            {
                foreach (BlockCutscene b in MGBlocksScript)
                {
                    b.enabled = false;
                }
                //End cutscene
                StartCoroutine(EndCutMiniGame());
            }
        }
        if (sentenceBlocks[14].transform.position == sentenceBlockLocations[14].position && !stopCallingMyCoroutine)
        {
            stopCallingMyCoroutine = true;
            StartCoroutine(waitForRead());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerRoomOneDetection>().blocksFound == 5)
        {
            //AkSoundEngine.PostEvent("StopChildhoodMusic", floor);
            //AkSoundEngine.PostEvent("GrowthStinger", floor);
            cutScene();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerRoomOneDetection>().blocksFound < 5)
            {
                notificationText.text = "Missing some Blocks";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Get blockcount from player
            //If block count != 5
            if (other.GetComponent<PlayerRoomOneDetection>().blocksFound < 5)
            {
                notificationMessage = "";
            }
            
        }
    }

    private void cutScene()
    {
        if (cutScenePlaying)
        {
            StartCoroutine(EndCutMiniGame());
        }
        else
        {
            //Start of cutscene
            MiniGame();
            cutScenePlaying = true;
            //>
            if(perspectiveSwap != null)
            {
                perspectiveSwap.cutSceneActive = true;
            }
            //>
            playerCamera.SetActive(false);
            controller.lock_movement = true;
            cutSceneCamera.SetActive(true);
            //This is a last minute fix, sometimes you could touch the box during the cutscene and it would change back
            //to the cut scene camera, and we dont want that
            GetComponent<BoxCollider>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private IEnumerator waitForRead()
    {
        yield return new WaitForSeconds(3);
        
        //cutScene();
        yield return null;
    }

    private void MiniGame()
    {
        foreach (GameObject block in cutsceneBlocks)
        {
            block.SetActive(true);
        }
        BlockMiniGameCanvas.SetActive(true);
        playerUI.SetActive(false);
    }

    private IEnumerator EndCutMiniGame()
    {
        //Get rid of Minigame Canvas
        DOOR.GetComponent<Animator>().SetTrigger("OpenIn");
        DOOR.GetComponent<AudioSource>().Play();
        victorySound.Play();
        ended = true;
        //argument.Stop();
        decreaseAudio = true;
        BlockMiniGameCanvas.SetActive(false);
        //Particles

        MGFinish.Play();
        
        yield return new WaitForSeconds(3);
        //>
        //Perspective swap stuff
        if (perspectiveSwap != null)
        {
            perspectiveSwap.cutSceneActive = false;
        }
        //>
        controller.lock_movement = false;
        cutScenePlaying = false;
        playerCamera.SetActive(true);
        cutSceneCamera.SetActive(false);
        playerUI.SetActive(true);
        
        
        yield return null;
    }
    
}
