using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookScript : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        //Open Door
        if (door != null)
        {
            door.GetComponent<Animator>().SetTrigger("DoorOpenIn");
            door.GetComponent<AudioSource>().Play();
        }

        //Activate any UI
    }
}
