using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveSwap : MonoBehaviour
{
    public bool playerActive;
    public GameObject player;
    public GameObject playerCam;
    public GameObject toybox;
    public GameObject journal;
    public GameObject options;
    public GameObject puzzleLocker;

    // Start is called before the first frame update
    void Start()
    {
        disableVantagePoints();
        // setPlayerState(playerActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
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

        if (toybox != null && toybox.GetComponent<ToyBox>().cutScenePlaying)
        {
            // player is active
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
        }
        else if (puzzleLocker != null && puzzleLocker.GetComponent<TriggerJigsaw>().inPuzzle)
        {
            player.GetComponent<PlayerController>().lock_movement = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (playerActive)
        {
            player.GetComponent<PlayerController>().lock_movement = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // player is active
            disableVantagePoints();
        }
        else
        {
            GameObject[] vantagePoints = bestRoom.GetComponent<RoomScript>().vantagePoints;

            if (vantagePoints.Length > 0)
            {
                player.GetComponent<PlayerController>().lock_movement = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                int currentPoint = bestRoom.GetComponent<RoomScript>().currentView;
                if (Input.GetKeyDown(KeyCode.D))
                {
                    --currentPoint;
                    if (currentPoint < 0)
                    {
                        currentPoint += vantagePoints.Length;
                    }
                    //print(currentPoint);
                }
                else if (Input.GetKeyDown(KeyCode.A))
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
            }
            else
            {
                print("NO VANTAGE POINTS");
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
