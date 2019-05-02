using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{

    public AudioSource[] audioStuff;
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (AudioSource a in audioStuff)
            {
                a.Play();
            }
        }
    }
}
