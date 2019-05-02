using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDoorScript : MonoBehaviour
{
    public Level2Script counter;
    public bool opened;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.pagesFound >= 3 && !opened)
        {
            opened = true;
            GetComponent<Animator>().SetTrigger("DoorOpenOut");
            GetComponent<AudioSource>().Play();
        }
    }
}
