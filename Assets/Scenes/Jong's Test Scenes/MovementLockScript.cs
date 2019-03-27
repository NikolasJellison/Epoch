using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLockScript : MonoBehaviour
{
    public GameObject player;
    public GameObject[] locks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerController>().manipulating)
        {
            foreach(GameObject movLock in locks){
                movLock.SetActive(true);
            }
        } else
        {
            foreach (GameObject movLock in locks)
            {
                movLock.SetActive(false);
            }
        }
    }
}
