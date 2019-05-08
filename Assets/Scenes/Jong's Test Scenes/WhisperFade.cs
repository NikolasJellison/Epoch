using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperFade : MonoBehaviour
{
    public bool start;
    public AudioSource whisper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start && whisper.volume > 0)
        {
            float volume = whisper.volume;
            volume -= 0.25f*Time.deltaTime;
            whisper.volume = Mathf.Max(0.0f, volume);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            start = true;
        }
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            start = true;
        }
    }
}
