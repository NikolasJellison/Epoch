using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyBox : MonoBehaviour
{
    //This is becoming the gameManager for room 1
    public GameObject cutSceneCamera;
    public GameObject playerCamera;
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
    public GameObject materialObject;
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

    private void Start()
    {
        BlockMiniGameCanvas.SetActive(false);
    }

    private void Update()
    {
        
        if(materialObject.GetComponent<MeshRenderer>().material.GetFloat("_ToyBoxGlow") == 0 && player.GetComponent<PlayerRoomOneDetection>().blocksFound == 5)
        {
            materialObject.GetComponent<MeshRenderer>().material.SetFloat("_ToyBoxGlow", 1);
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
            AkSoundEngine.PostEvent("StopChildhoodMusic", floor);
            AkSoundEngine.PostEvent("GrowthStinger", floor);
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
            playerCamera.SetActive(false);
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
        DOOR.GetComponent<Animator>().SetTrigger("OpenIn");
        DOOR.GetComponent<AudioSource>().Play();
        //cutScene();
        yield return null;
    }

    private void MiniGame()
    {
        BlockMiniGameCanvas.SetActive(true);
        playerUI.SetActive(false);
    }

    private IEnumerator EndCutMiniGame()
    {
        //Get rid of Minigame Canvas
        BlockMiniGameCanvas.SetActive(false);
        //Particles
        MGFinish.Play();
        yield return new WaitForSeconds(3);
        cutScenePlaying = false;
        playerCamera.SetActive(true);
        cutSceneCamera.SetActive(false);
        yield return null;
    }

    
}
