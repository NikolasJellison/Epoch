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
        if (!options.activeSelf && swapEnabled && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            playerActive = !playerActive;
            AudioSource swapSound = GetComponent<AudioSource>();
            if (swapSound != null)
            {
                //swapSound.Play();
            }
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

        if ((toybox != null && toybox.GetComponent<ToyBox>().cutScenePlaying) || cutSceneActive)
        {
            // player is active
            RoomUI.text = "";
            ViewUI.text = "";
            disableVantagePoints();
        }
        else if(options != null && options.activeSelf)
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
            /*
            player.GetComponent<PlayerController>().lock_movement = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //*/
            RoomUI.text = "";
            ViewUI.text = "";
        }
        else if (playerActive)
        {
            player.GetComponent<PlayerController>().lock_movement = false;
            playerCam.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // player is active
            //Check if the cutscene is going on

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
                // ----
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

    public void nextRoom()
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

        if (vantagePoints.Length > 0)
        {
            int currentPoint = bestRoom.GetComponent<RoomScript>().currentView;

            --currentPoint;
            if (currentPoint < 0)
            {
                currentPoint += vantagePoints.Length;
            }

            bestRoom.GetComponent<RoomScript>().currentView = currentPoint;

            if (!vantagePoints[currentPoint].activeSelf)
            {
                disableVantagePoints();
                vantagePoints[currentPoint].SetActive(true);
            }

            ViewUI.text = "Window " + (currentPoint + 1) + "/" + vantagePoints.Length;
        }
    }

    public void prevRoom()
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
        if (vantagePoints.Length > 0)
        {
            int currentPoint = bestRoom.GetComponent<RoomScript>().currentView;

            currentPoint = (currentPoint + 1) % vantagePoints.Length;

            bestRoom.GetComponent<RoomScript>().currentView = currentPoint;

            if (!vantagePoints[currentPoint].activeSelf)
            {
                disableVantagePoints();
                vantagePoints[currentPoint].SetActive(true);
            }

            ViewUI.text = "Window " + (currentPoint + 1) + "/" + vantagePoints.Length;
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
