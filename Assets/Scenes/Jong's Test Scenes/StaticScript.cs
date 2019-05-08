using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticScript : MonoBehaviour
{
    public TVMountScript tv;
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
        float currVol = GetComponent<AudioSource>().volume;
        if (tv.start)
        {
            GetComponent<AudioSource>().Stop();
            gameObject.SetActive(false);
        }
        
        float distance = Vector3.Distance(tv.gameObject.transform.position, player.position);


        if (distance >= thresholdDist)
        {
            GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            float volume = maxVolume * ((thresholdDist - distance) / thresholdDist);
            GetComponent<AudioSource>().volume = volume;
        }
        //GetComponent<AudioSource>().volume
        /*
        if (inContact && currVol < maxVolume)
        {
            GetComponent<AudioSource>().volume = Mathf.Min(maxVolume, currVol + 0.5f * Time.deltaTime);
        }
        //*/


    }
}
