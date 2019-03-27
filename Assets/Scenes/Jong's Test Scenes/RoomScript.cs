using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public GameObject[] vantagePoints;
    public int currentView;
    public int roomId;
    public string roomName;
    // Start is called before the first frame update
    void Start()
    {
        currentView = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
