using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSoundScript : MonoBehaviour
{
    public float maxVolume;
    public float thresholdDist = 10;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VisionObjectScript>().enabled)
        {
            GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            float distance = Vector3.Distance(gameObject.transform.position, player.position);
            float volume = maxVolume * ((thresholdDist - distance) / thresholdDist);
            GetComponent<AudioSource>().volume = volume;
        }
    }
}
