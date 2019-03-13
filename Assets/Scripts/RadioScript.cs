using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    public GameObject speaker;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        if(speaker == null)
        {
            speaker = gameObject;
        }
        if (level == 1)
        {
            AkSoundEngine.PostEvent("PlayChildhoodMusic", speaker);

        }
        else if (level == 2)
        {
            AkSoundEngine.PostEvent("PlayTeenageMusic", speaker);
        }
        else
        {
            AkSoundEngine.PostEvent("PlayAdultMusic", speaker);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
