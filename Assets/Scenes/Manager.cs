using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Transform[] spawns;
    public int currentSpawn;
    public Transform[] lockers;
    // Start is called before the first frame update
    void Start()
    {
        currentSpawn = 0;
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
            if(locker.closedFirst)
            {
                ++currentSpawn;
            }
        }

        // If we fall off the stage or are detected, move an object to that place.
    }
}
