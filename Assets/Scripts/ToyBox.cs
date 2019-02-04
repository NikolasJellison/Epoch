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
    private bool cutScenePlaying;
    public GameObject DOOR;
    private bool stopCallingMyCoroutine;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            cutScene();
        }
        step = speed * Time.deltaTime;
        if (cutScenePlaying)
        {
            cutSceneCamera.transform.LookAt(sentenceBlocks[7].transform.position);
            for(int i = 0; i < sentenceBlocks.Length; i++)
            {
                sentenceBlocks[i].transform.position = Vector3.MoveTowards(sentenceBlocks[i].transform.position, sentenceBlockLocations[i].position, step);
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
            cutScenePlaying = false;
            playerCamera.SetActive(true);
            cutSceneCamera.SetActive(false);
        }
        else
        {
            cutScenePlaying = true;
            playerCamera.SetActive(false);
            cutSceneCamera.SetActive(true);
        }
    }

    private IEnumerator waitForRead()
    {
        yield return new WaitForSeconds(3);
        DOOR.GetComponent<Animator>().SetTrigger("OpenIn");
        DOOR.GetComponent<AudioSource>().Play();
        cutScene();
        yield return null;
    }
}
