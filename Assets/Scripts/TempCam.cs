using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCam : MonoBehaviour
{
    public Transform playerCam;
    public float defaultDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        transform.LookAt(playerCam, new Vector3(0,1,0));
        int mask = 1 << 2;
        mask = ~mask;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            Vector3 moveVec = defaultDistance * transform.forward;
            if(hit.distance < defaultDistance)
            {
                float newCoeff = Mathf.Max(0.00001f, (hit.distance - 0.05f));
                
                moveVec = newCoeff * transform.forward;
            }
            playerCam.position = transform.position + moveVec;
        }
        
    }
}
