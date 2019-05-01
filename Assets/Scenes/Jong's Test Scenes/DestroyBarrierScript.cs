using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrierScript : MonoBehaviour
{
    public DestroyPromptScript destroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy.triggeredDestruction)
        {
            gameObject.SetActive(false);
        }
    }
}
