using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("PlayChildhoodMusic", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
