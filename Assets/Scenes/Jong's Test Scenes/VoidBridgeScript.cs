using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBridgeScript : MonoBehaviour
{
    public VisionObjectScript bridge;
    public MovementLockScript mgr;
    public GameObject blocker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!bridge.enabled)
        {
            mgr.locks.Remove(blocker);
            Destroy(blocker);
        }
        //*/
    }
}
