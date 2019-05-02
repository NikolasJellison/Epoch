using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollision : MonoBehaviour
{
    public Transform playerCam;
    public float defaultDistance;
    public float minDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This will make the object face the player camera at all times        
        transform.LookAt(playerCam, new Vector3(0,1,0));

        // This mask makes it so objects with on the "Ignore Raycast" layer (2) don't affect the ray. 
        // The layer a gameobject is on is seen in the inspector with the index 
        int mask = 1 << 2;
        mask = ~mask;

        // Raycast hit objects store information about where the ray hit
        RaycastHit hit;
        // Shoots a ray from this object forward (which will be towards the camera), storing the ending point info in hit
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask))
        {
            // Simply draws the ray that was shot
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            
            // Creates a vector that will place the camera the default distance away from the object
            Vector3 moveVec = defaultDistance * transform.forward;

            // If we are close than the default distance...
            if(hit.distance < defaultDistance)
            {
                // Change the distance to something smaller. 
                // The - 0.05f is so we aren't placing the camera right at the raycast hit
                float newCoeff = Mathf.Max(minDistance, (hit.distance - 0.05f));
                moveVec = newCoeff * transform.forward;
            }
            // Moves the player camera accordingly
            playerCam.position = transform.position + moveVec;
        }
        
    }
}
