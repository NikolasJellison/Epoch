using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwapScript : MonoBehaviour
{
    public PerspectiveSwap vantageManager;
    public GameObject[] roomArrows;
    public GameObject[] UI;
    public bool hideArrows;
    public GameObject options;
    public GameObject[] goalUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vantageManager.playerActive)
        {
            foreach(GameObject arrow in roomArrows)
            {
                arrow.SetActive(false);
            }
            foreach(GameObject ui in UI)
            {
                if(ui != null)
                {
                    ui.SetActive(true);
                }
                
            }
        }
        else
        {
            foreach (GameObject ui in UI)
            {
                ui.SetActive(false);
            }
            RoomSelectorScript selector = vantageManager.player.GetComponent<RoomSelectorScript>();
            GameObject bestRoom = selector.rooms[0];
            for (int i = 1; i < selector.rooms.Count; ++i)
            {
                if (bestRoom.GetComponent<RoomScript>().roomId < selector.rooms[i].GetComponent<RoomScript>().roomId)
                {
                    bestRoom = selector.rooms[i];
                }
            }

            if (!hideArrows && !options.activeSelf && bestRoom.GetComponent<RoomScript>().vantagePoints.Length > 1)
            {
                foreach (GameObject arrow in roomArrows)
                {
                    arrow.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject arrow in roomArrows)
                {
                    arrow.SetActive(false);
                }
            }
        }
        foreach (GameObject ui in goalUI)
        {
            ui.SetActive(!options.activeSelf);
            
        }


    }
}
