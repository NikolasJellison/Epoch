using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTiles : MonoBehaviour
{
    //public Transform tiles;
    public bool tilesOn;
    // Start is called before the first frame update
    void Start()
    {
        tilesOn = false;
        int numChildren = gameObject.transform.childCount;
        for (int i = 0; i < numChildren; ++i)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!tilesOn && Input.GetKeyUp(KeyCode.T))
        {
            tilesOn = true;
            int numChildren = gameObject.transform.childCount;
            for (int i = 0; i < numChildren; ++i)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

        
}
