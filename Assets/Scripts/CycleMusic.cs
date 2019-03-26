using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleMusic : MonoBehaviour
{
    public AudioClip[] tracks;
    public int track_num;
    private bool dirty;
    // Start is called before the first frame update
    void Start()
    {
        dirty = false;
        track_num = 0;
        GetComponent<AudioSource>().clip = tracks[track_num];
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            track_num = (track_num + 1) % tracks.Length;
            dirty = true;
        }

        if(dirty)
        {
            dirty = false;
            GetComponent<AudioSource>().clip = tracks[track_num];
            GetComponent<AudioSource>().Play();
        }
    }
}
