using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("PlayChildhoodMusic", floor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
