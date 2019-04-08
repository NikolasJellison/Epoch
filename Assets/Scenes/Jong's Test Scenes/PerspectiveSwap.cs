using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerspectiveSwap : MonoBehaviour
{
    public bool playerActive;
    public GameObject player;
    public GameObject playerCam;
    public GameObject toybox;
    public GameObject journal;
    public bool swapEnabled;
    public bool newViewEnabled;
    public GameObject options;
    public GameObject puzzleLocker;
    public Text RoomUI;
    public Text ViewUI;
    public bool cutSceneActive;


    // Start is called before the first frame update
    void Start()
    {
        disableVantagePoints();
        // setPlayerState(playerActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (swapEnabled && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            playerActive = !playerActive;
        }

        RoomSelectorScript selector = player.GetComponent<RoomSelectorScript>();
        GameObject bestRoom = selector.rooms[0];
        for (int i = 1; i < selector.rooms.Count; ++i)
        {
            if (bestRoom.GetComponent<RoomScript>().roomId < selector.rooms[i].GetComponent<RoomScript>().roomId)
            {
                bestRoom = selector.rooms[i];
            }
        }
        // set Room UI
        RoomUI.text = bestRoom.GetComponent<RoomScript>().roomName;

        if (toybox != null && toybox.GetComponent<ToyBox>().cutScenePlaying)
        {
            // player is active
            RoomUI.text = "";
            ViewUI.text = "";
            disableVantagePoints();
        }
        else if((journal != null && journal.activeSelf) || (options != null && options.activeSelf))
        {
            // Essentially, allow the playerController script to handle setting the movement and cursor
            /*
            player.GetComponent<PlayerController>().lock_movement = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //*/
            RoomUI.text = "";
            ViewUI.text = "";
        }
        else if (puzzleLocker != null && puzzleLocker.GetComponent<TriggerJigsaw>().inPuzzle)
        {
            player.GetComponent<PlayerController>().lock_movement = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            RoomUI.text = "";
            ViewUI.text = "";
        }
        else if (playerActive)
        {
            player.GetComponent<PlayerController>().lock_movement = false;
            
            // player is active
            //Check if the cutscene is going on
            if (!cutSceneActive)
            {
                playerCam.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            disableVantagePoints();
            // deactivate View UI
            ViewUI.text = "";
        }
        else
        {

            GameObject[] vantagePoints = bestRoom.GetComponent<RoomScript>().vantagePoints;

            if (vantagePoints.Length > 0)
            {
                player.GetComponent<PlayerController>().lock_movement = true;
                playerCam.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                int currentPoint = bestRoom.GetComponent<RoomScript>().currentView;
                if (newViewEnabled && Input.GetKeyDown(KeyCode.D))
                {
                    --currentPoint;
                    if (currentPoint < 0)
                    {
                        currentPoint += vantagePoints.Length;
                    }
                    //print(currentPoint);
                }
                else if (newViewEnabled && Input.GetKeyDown(KeyCode.A))
                {
                    currentPoint = (currentPoint + 1) % vantagePoints.Length;
                    //print(currentPoint);
                }
                bestRoom.GetComponent<RoomScript>().currentView = currentPoint;
                if (!vantagePoints[currentPoint].activeSelf)
                {
                    disableVantagePoints();
                    vantagePoints[currentPoint].SetActive(true);
                }
                // activate view UI
                ViewUI.text = "Window " + (currentPoint+1) + "/" + vantagePoints.Length;
            }
            else
            {
                print("NO VANTAGE POINTS");
                ViewUI.text = "";
            }
        }



    }


    void disableVantagePoints()
    {
        RoomSelectorScript selector = player.GetComponent<RoomSelectorScript>();
        GameObject bestRoom = selector.rooms[0];
        for (int i = 1; i < selector.rooms.Count; ++i)
        {
            if (bestRoom.GetComponent<RoomScript>().roomId < selector.rooms[i].GetComponent<RoomScript>().roomId)
            {
                bestRoom = selector.rooms[i];
            }
        }
        GameObject[] vantagePoints = bestRoom.GetComponent<RoomScript>().vantagePoints;
        foreach (GameObject vantagePoint in vantagePoints)
        {
            vantagePoint.SetActive(false);
        }
    }
    /*
    void setPlayerState(bool state)
    {
        if (player.GetComponent<PlayerController>().enabled != state)
        {
            player.GetComponent<PlayerController>().enabled = state;
        }
        if (playerCam.activeSelf != state)
        {
            playerCam.SetActive(state);
        }
        Cursor.visible = !state;
        if(state)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    //*/
}
