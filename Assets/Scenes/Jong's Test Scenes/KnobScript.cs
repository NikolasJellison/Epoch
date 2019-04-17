using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobScript : MonoBehaviour
{
    public bool start;
    public Transform knob;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        start = true;
        if (start)
        {
            knob.parent = door.transform;
            door.GetComponent<Animator>().SetTrigger("DoorOpenOut");
            door.GetComponent<AudioSource>().Play();
        }
    }
}
