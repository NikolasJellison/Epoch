using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperScript : MonoBehaviour
{
    public AudioSource ambiance;
    public bool trigger;
    public bool played;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            if(ambiance.volume > 0)
            {
                float volume = ambiance.volume;
                volume -= 0.07f * Time.deltaTime;
                ambiance.volume = Mathf.Max(0.0f, volume);
            }

            if (!played)
            {
                played = true;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            trigger = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            trigger = true;
        }
    }
}
