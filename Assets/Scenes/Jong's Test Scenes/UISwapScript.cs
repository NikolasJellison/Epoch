using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwapScript : MonoBehaviour
{
    public PerspectiveSwap vantageManager;
    public GameObject[] roomArrows;
    public bool hideArrows;
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
        }
        else
        {
            RoomSelectorScript selector = vantageManager.player.GetComponent<RoomSelectorScript>();
            GameObject bestRoom = selector.rooms[0];
            for (int i = 1; i < selector.rooms.Count; ++i)
            {
                if (bestRoom.GetComponent<RoomScript>().roomId < selector.rooms[i].GetComponent<RoomScript>().roomId)
                {
                    bestRoom = selector.rooms[i];
                }
            }

            if (!hideArrows && bestRoom.GetComponent<RoomScript>().vantagePoints.Length > 1)
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
    }
}
