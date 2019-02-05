using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Transform[] spawns;
    public int currentSpawn;
    public Transform[] lockers;
    public Transform[] checkPointTiles;
    public GameObject laptop;
    public GameObject door;
    public bool doorOpen;
    public Transform player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        currentSpawn = 0;
        doorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {   
        /*
        print(locker.closedFirst);
        //*/
        
        if (currentSpawn != 3)
        {
            Locker locker = lockers[currentSpawn+1].GetChild(7).GetChild(1).GetChild(2).GetComponent<Locker>();
            if (locker.closedFirst)
            {
                ++currentSpawn;
                checkPointTiles[currentSpawn].gameObject.SetActive(true);
            }
        }

        if(laptop == null && !doorOpen)
        {
            doorOpen = true;
            door.GetComponent<Animator>().SetTrigger("OpenOut");
        }
        // If we fall off the stage or are detected, move an object to that place.
        // Player goes below a certain Y value, set to spawn
        Vector3 player_pos = player.position;
        bool fell_off = player_pos[1] < -30;
        bool detected = enemy.GetComponent<EnemyPathing>().alertLevel >= 100;

        if (detected || fell_off)
        {

            player.GetComponent<Rigidbody>().velocity = Vector3.zero;   
            player.position = spawns[currentSpawn].position;
        }
        // Array of enemies
            // If any reach an alert level of 100, set to spawn

    }
}
