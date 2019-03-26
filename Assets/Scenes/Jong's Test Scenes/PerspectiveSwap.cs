using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveSwap : MonoBehaviour
{
    public bool playerActive;
    public GameObject player;
    public GameObject playerCam;
    public GameObject[] vantagePoints;
    public int currentPoint;
    public GameObject toybox;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        disableVantagePoints();
        setPlayerState(playerActive);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            playerActive = !playerActive;
        }

        if (toybox.GetComponent<ToyBox>().cutScenePlaying)
        {
            disableVantagePoints();
        }
        else if (playerActive)
        {
            setPlayerState(true);
            disableVantagePoints();
        }
        else
        {
            if (vantagePoints.Length > 0)
            {
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

                setPlayerState(false);
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
        foreach(GameObject vantagePoint in vantagePoints)
        {
            vantagePoint.SetActive(false);
        }
    }

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
}
